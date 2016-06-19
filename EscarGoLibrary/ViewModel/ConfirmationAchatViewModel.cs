using EscarGoLibrary.Models;
using System;

namespace EscarGoLibrary.ViewModel
{
    public class ConfirmationAchatViewModel
    {
        public Course Course { get; set; }
        public DateTime DateAchat { get; set; }
        public int NbTickets { get; set; }
        public string Message { get; set; }
        public bool EstEnregistre { get; set; }
    }
}
