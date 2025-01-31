using System.ComponentModel;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using CommunityToolkit.Maui.Views;

namespace MetaRov;

public partial class Quiz : ContentPage
{
    public Quiz()
    {
        InitializeComponent();
        BindingContext = new QuizViewModel(this);
        NavigationPage.SetHasNavigationBar(this, false);
    }
}
public class QuizViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;
    private readonly ContentPage _page; // ✅ ใช้เรียก Modal Popup
    private int currentQuestionIndex = 0;
    private readonly List<string> userAnswers = new();
    private double _progressValue = 0.0;

    private string _selectedOption;
    public string SelectedOption
    {
        get => _selectedOption;
        set
        {
            _selectedOption = value;
            OnPropertyChanged(nameof(SelectedOption));
        }
    }

    public double ProgressValue
    {
        get => _progressValue;
        set
        {
            _progressValue = value;
            OnPropertyChanged(nameof(ProgressValue));
        }
    }

    private readonly string[] Questions = new[]
    {
        "เมื่อคุณต้องทำงานในกลุ่ม คุณมักจะ...?",
        "ในสถานการณ์ที่ต้องเลือกการโจมตีอย่างเร็ว คุณจะเลือก...?",
        "คุณชอบทำอะไรในเวลาว่าง?",
        "เมื่อเผชิญกับปัญหายากๆ คุณจะ...?",
        "คุณเป็นคนที่..."
    };

    private readonly (string A, string B, string C)[] Options = new[]
    {
        ("A) นำทางและชี้แนะกลุ่ม", "B) คอยสนับสนุนและช่วยเหลือทีม", "C) ทำงานคนเดียวได้ดีที่สุด"),
        ("A) เข้าประจันหน้าและโจมตีทันที", "B) ระวังตัวและคอยดูท่าที", "C) เลือกวิธีการโจมตีที่มีระยะห่าง"),
        ("A) ทำกิจกรรมที่ท้าทายและน่าตื่นเต้น", "B) ทำกิจกรรมที่ผ่อนคลายและเป็นมิตร", "C) ใช้เวลาในการคิดหรือทำกิจกรรมส่วนตัว"),
        ("A) ตัดสินใจและทำทันที", "B) คิดและพิจารณาผลลัพธ์ระยะยาว", "C) ขอคำแนะนำจากคนอื่น"),
        ("A) กล้าหาญและไม่กลัวความเสี่ยง", "B) คำนึงถึงความปลอดภัยและความมั่นคง", "C) ชอบที่จะมีทางเลือกหลายๆ ทาง")
    };

    public string CurrentQuestion => Questions[currentQuestionIndex];
    public string OptionA => Options[currentQuestionIndex].A;
    public string OptionB => Options[currentQuestionIndex].B;
    public string OptionC => Options[currentQuestionIndex].C;
    public string NextButtonText => currentQuestionIndex < Questions.Length - 1 ? "ถัดไป" : "ยืนยัน";
    public bool IsNextEnabled => userAnswers.Count > currentQuestionIndex;

    public Command NextQuestionCommand { get; }
    public Command<string> SelectOptionCommand { get; }

    public QuizViewModel(ContentPage page)
    {
        _page = page; // ✅ ใช้ _page เพื่อเรียก Modal
        NextQuestionCommand = new Command(NextQuestion);
        SelectOptionCommand = new Command<string>(SelectOption);
    }

    private void NextQuestion()
    {
        if (currentQuestionIndex < Questions.Length - 1)
        {
            currentQuestionIndex++;
            ProgressValue = (double)(currentQuestionIndex + 1) / Questions.Length;
            SelectedOption = ""; // ✅ รีเซ็ตค่าตัวเลือกเมื่อเปลี่ยนคำถาม
            NotifyPropertiesChanged();
        }
        else
        {
            ShowResult();
        }
    }

    private void SelectOption(string option)
    {
        SelectedOption = option; // ✅ อัปเดตค่าตัวเลือกที่ถูกเลือก

        if (userAnswers.Count > currentQuestionIndex)
        {
            userAnswers[currentQuestionIndex] = option;
        }
        else
        {
            userAnswers.Add(option);
        }

        NotifyPropertiesChanged();
    }

    private async void ShowResult()
    {
        string matchedHero = MatchCharacter(userAnswers);
        string imagePath = $"heroes/{matchedHero.ToLower()}.png";
        string description = $"พบว่าคุณมีลักษณะนิสัยใกล้เคียงกับ {matchedHero}";

        // ✅ ใช้ Modal Popup แทน DisplayAlert
        _page.ShowPopup(new HeroPopup(matchedHero, imagePath, description));
    }

    private void NotifyPropertiesChanged()
    {
        OnPropertyChanged(nameof(CurrentQuestion));
        OnPropertyChanged(nameof(OptionA));
        OnPropertyChanged(nameof(OptionB));
        OnPropertyChanged(nameof(OptionC));
        OnPropertyChanged(nameof(NextButtonText));
        OnPropertyChanged(nameof(IsNextEnabled));
        OnPropertyChanged(nameof(ProgressValue));
        OnPropertyChanged(nameof(SelectedOption)); // ✅ อัปเดต UI เมื่อมีการเลือกตัวเลือก
    }

    private string MatchCharacter(List<string> answers)
    {
        Dictionary<string, int> scores = new Dictionary<string, int>()
    {
        { "Florentino", 0 }, { "Omen", 0 }, { "Keera", 0 }, { "Aoi", 0 },
        { "Iggy", 0 }, { "Liliana", 0 }, { "Valhein", 0 }, { "Fennik", 0 },
        { "Teemee", 0 }, { "Y'Bneth", 0 }
    };

        for (int i = 0; i < answers.Count; i++)
        {
            string ans = answers[i];
            switch (i)
            {
                case 0: // ✅ คำถามข้อที่ 1
                    if (ans == "A") { scores["Florentino"]++; scores["Omen"]++; }
                    else if (ans == "B") { scores["Teemee"]++; scores["Y'Bneth"]++; scores["Liliana"]++; }
                    else if (ans == "C") { scores["Aoi"]++; scores["Keera"]++; scores["Iggy"]++; }
                    break;

                case 1: // ✅ คำถามข้อที่ 2
                    if (ans == "A") { scores["Florentino"]++; scores["Fennik"]++; scores["Keera"]++; }
                    else if (ans == "B") { scores["Y'Bneth"]++; scores["Iggy"]++; scores["Liliana"]++; scores["Teemee"]++; }
                    else if (ans == "C") { scores["Valhein"]++; scores["Iggy"]++; scores["Aoi"]++; }
                    break;

                case 2: // ✅ คำถามข้อที่ 3
                    if (ans == "A") { scores["Florentino"]++; scores["Fennik"]++; scores["Keera"]++; scores["Aoi"]++; }
                    else if (ans == "B") { scores["Teemee"]++; scores["Liliana"]++; scores["Y'Bneth"]++; scores["Iggy"]++; }
                    else if (ans == "C") { scores["Aoi"]++; scores["Iggy"]++; scores["Valhein"]++; }
                    break;

                case 3: // ✅ คำถามข้อที่ 4
                    if (ans == "A") { scores["Florentino"]++; scores["Omen"]++; scores["Keera"]++; scores["Fennik"]++; }
                    else if (ans == "B") { scores["Iggy"]++; scores["Liliana"]++; scores["Valhein"]++; scores["Teemee"]++; }
                    else if (ans == "C") { scores["Teemee"]++; scores["Y'Bneth"]++; scores["Aoi"]++; scores["Keera"]++; }
                    break;

                case 4: // ✅ คำถามข้อที่ 5
                    if (ans == "A") { scores["Florentino"]++; scores["Keera"]++; scores["Omen"]++; scores["Fennik"]++; }
                    else if (ans == "B") { scores["Iggy"]++; scores["Valhein"]++; scores["Teemee"]++; scores["Y'Bneth"]++; }
                    else if (ans == "C") { scores["Aoi"]++; scores["Liliana"]++; scores["Y'Bneth"]++; scores["Iggy"]++; }
                    break;
            }
        }

        // ✅ หาฮีโร่ที่มีคะแนนสูงสุด
        return scores.Aggregate((l, r) => l.Value > r.Value ? l : r).Key;
    }

    private void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}