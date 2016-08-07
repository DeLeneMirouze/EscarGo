#region using
using EscarGoLibrary.Repositories;
using EscarGoLibrary.Repositories.Async;
using EscarGoLibrary.Storage.Repository;
using Microsoft.Azure.WebJobs;
using System;
using System.IO;
using System.Threading.Tasks;
#endregion

namespace QueueJobs
{
    public class Functions
    {
        #region Constructeur
        static readonly IQueueRepositoryAsync _queueRepositoryAsync;
        static readonly ITicketRepositoryAsync _ticketRepositoryAsync;
        static EscarGoContext context;

        static Functions()
        {
            context = new EscarGoContext();
            _queueRepositoryAsync = new QueueRepositoryAsync();
            _ticketRepositoryAsync = new TicketRepository(context);
        }
        #endregion

        #region ProcessTicketSales
        [NoAutomaticTrigger]
        public static async Task ProcessTicketSales(TextWriter log)
        {
            try
            {
                while (true)
                {
                    string message = await _queueRepositoryAsync.ReadMessageAsync();
                    if (string.IsNullOrWhiteSpace(message))
                    {
                        await log.WriteLineAsync("Pas de message");
                        break;
                    }

                    string[] splitted = message.Split(',');

                    int courseId = Convert.ToInt32(splitted[0]);
                    int visiteurId = Convert.ToInt32(splitted[1]);
                    int nbPlaces = Convert.ToInt32(splitted[2]);
                    await _ticketRepositoryAsync.AddTicketAsync(courseId, visiteurId, nbPlaces);

                    await log.WriteLineAsync("Enregistrement de la vente réussie");
                }
            }
            catch (Exception ex)
            {
                await log.WriteLineAsync(ex.Message);
            }
        }
        #endregion
    }
}
