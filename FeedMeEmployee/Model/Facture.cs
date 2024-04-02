using System.Globalization;

namespace FeedMeEmployee.Model
{
    public sealed class Facture
    {
        public DateTime DateHeure { get; set; }
        public string NomCommerce { get; set; }
        public decimal Montant { get; set; }
        public string MontantFormatte
        {
            get
            {
                CultureInfo cultureInfo = new CultureInfo("fr-FR"); // Pour le français, ou "de-DE" pour l'allemand, etc.
                return Montant.ToString("C", cultureInfo);
            }
        }
    }
}
