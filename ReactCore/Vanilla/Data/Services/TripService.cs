using System.Collections.Generic;
using System.Linq;

namespace ReactCore.Data
{
    public class TripService : ITripService
    {
        public List<Trip> GetAllTrips() => Data.Trips.ToList();
        public Trip GetTripById(int id) => Data.Trips.FirstOrDefault(p => p.Id == id);
        
        public void UpdateTrip(int id, Trip trip)
        {
            var oldTrip = Data.Trips.FirstOrDefault(p => p.Id == id);
            if(oldTrip != null)
            {
                oldTrip.Name = trip.Name;
                oldTrip.Description = trip.Description;
                oldTrip.DateStarted = trip.DateStarted;
                oldTrip.DateCompleted = trip.DateCompleted;
            }
        }
        public void DeleteTrip(int id)
        {
            var trip = Data.Trips.FirstOrDefault(p => p.Id == id);
            if(trip != null)
            {
                Data.Trips.Remove(trip); 
            }
        }
        public void AddTrip(Trip trip)
        {
            Data.Trips.Add(trip);
        }
    }
}