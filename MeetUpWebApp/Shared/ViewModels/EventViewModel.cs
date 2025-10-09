using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;

namespace MeetUpWebApp.Shared.ViewModels
{
    public class EventViewModel
    {
        public int EventId { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 5)]
        public string? Title { get; set; }

        [StringLength(1000, MinimumLength = 10)]
        public string? Description { get; set; }

        [Required]
        public DateOnly BeginDate { get; set; }

        [Required]
        public TimeOnly BeginTime { get; set; }

        [Required]
        public DateOnly EndDate { get; set; }

        [Required]
        public TimeOnly EndTime { get; set; }

        public string? Location { get; set; }

        public string? MeetupLink { get; set; }

        [Required]
        public string? Category { get; set; }

        [Range(0, int.MaxValue)]
        public int Capacity { get; set; }


        public IBrowserFile CoverImage { get; set; }

        public string? ImageUrl { get; set; }

        public int OrganizerId { get; set; }

        [Range(0, double.MaxValue)]
        public decimal? TicketPrice { get; set; }

        public bool Refundable { get; set; }


        private bool hasCost = false;

        public bool HasCost
        {
            get
            {
                if (TicketPrice > 0) return true;
                return hasCost;
            }
            set
            {
                hasCost = value;
                if (hasCost == false)
                {
                    TicketPrice = null;
                }
            }
        }


        public EventViewModel()
        {
            BeginDate = DateOnly.FromDateTime(DateTime.Now);
            EndDate = DateOnly.FromDateTime(DateTime.Now);
            BeginTime = TimeOnly.FromDateTime(DateTime.Now);
            EndTime = TimeOnly.FromDateTime(DateTime.Now);
        }

        public string? Validate()
        {
            string? errorMessage = string.Empty;
            errorMessage = ValidateDates();

            if (!string.IsNullOrWhiteSpace(errorMessage))
            {
                return errorMessage;
            }

            errorMessage = ValidateLocation();
            if (!string.IsNullOrWhiteSpace(errorMessage))
            {
                return errorMessage;
            }

            errorMessage = ValidateMeetUpLink();
            if (!string.IsNullOrWhiteSpace(errorMessage))
            {
                return errorMessage;
            }

            errorMessage = ValidateCoverImage();
            if (!string.IsNullOrWhiteSpace(errorMessage))
            {
                return errorMessage;
            }

            return string.Empty;

        }

        public string? ValidateCoverImage()
        {
            if (CoverImage == null && EventId <= 0)
            {
                return "Lütfen bir kapak fotoğrafı seçin.";
            }

            return string.Empty;
        }

        private string? ValidateDates()
        {
            if (BeginDate > EndDate)
            {
                return "Başlangıç tarihi bitiş tarihinden büyük olamaz.";
            }
            DateTime combinedBeginDateTime = new DateTime(BeginDate.Year, BeginDate.Month, BeginDate.Day, BeginTime.Hour, BeginTime.Minute, BeginTime.Second);
            DateTime combinedEndDateTime = new DateTime(EndDate.Year, EndDate.Month, EndDate.Day, EndTime.Hour, EndTime.Minute, EndTime.Second);
            if (combinedBeginDateTime < DateTime.Now)
            {
                return "Başlangıç tarihi ileri bir tarihte olmalı ";
            }
            if (combinedEndDateTime < DateTime.Now)
            {
                return "Bitiş tarihi geçmiş bir tarih olamaz.";
            }
            if (combinedEndDateTime - combinedBeginDateTime < TimeSpan.FromMinutes(30))
            {
                return "Etkinlik süresi en az 30 dakika olmalıdır.";
            }

            if (BeginDate.Year > DateTime.Now.Year)
            {
                return "Başlangıç tarihi bulunduğunuz yıl içinde olmalıdır.";
            }
            if (EndDate.Year > DateTime.Now.Year + 1)
            {
                return "Bitiş tarihi en fazla seneye olmalıdır.";
            }

            return string.Empty;
        }

        private string? ValidateLocation()
        {
            if (Category == MeetupCategoriesEnum.InPerson.ToString() && string.IsNullOrWhiteSpace(Location))
            {
                return "Yüzyüze etkinliklerde lokasyon zorunludur.";
            }
            return string.Empty;
        }

        private string? ValidateMeetUpLink()
        {
            if (Category == MeetupCategoriesEnum.Online.ToString() && string.IsNullOrWhiteSpace(MeetupLink))
            {
                return "Çevrimçi etkinliklerde link zorunludur.";
            }
            return string.Empty;
        }

    }
}
