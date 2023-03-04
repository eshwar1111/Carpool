using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using Carpool.Database;
using Carpool.Models;
using Carpool.Services.Contracts;
using System.Globalization;

namespace Carpool.Services
{
    public class BookRideService : IBookRideService
    {
        DatabaseContext CarpoolDb;
        public BookRideService(DatabaseContext CarpoolDb)
        {
            this.CarpoolDb = CarpoolDb;
        }

        public List<Stop> GetUpdatedStops(List<Stop> stops, int startPoint, int pathLength)
        {
            try
            {

                for (int i = startPoint; i < startPoint + pathLength; i++)
                {
                    stops[i].SeatsAtStop -= 1;
                }

                return stops;
            }
            catch (Exception ex)
            {
                return stops;
            }
        }

        public int GetAvailableSeats(List<Stop> stops, int startPoint, int endPoint)
        {
            try
            {

                int availableSeats = -1;
                for (int i = startPoint; i < startPoint + endPoint; i++)
                {
                    if (availableSeats == -1)
                    {
                        availableSeats = stops[i].SeatsAtStop;
                    }
                    availableSeats = Math.Min(stops[i].SeatsAtStop, availableSeats);
                }


                return availableSeats;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public List<CurrentRide> GetAvailableRides(BookRide ride)
        {
            try
            {
                List<CurrentRide> availableRides = new List<CurrentRide>();

                foreach (var availableRide in CarpoolDb.Rides.Include(r => r.Stops).ToList())
                {

                    if (availableRide.IsBooked == false)
                    {
                        List<int> PathDetails= IsPathExists(availableRide, ride.StartPoint, ride.EndPoint);
                        int startPoint =  PathDetails[0];
                        int pathLength = PathDetails[PathDetails.Count- 1];
                        

                        if (startPoint != -1 && availableRide.Date.Date.ToString() == ride.Date.Date.ToString() && availableRide.TimeSlot == ride.Time)
                        {
                            CurrentRide newAvailableRide = new CurrentRide()
                            {
                                StartPoint = ride.StartPoint,
                                EndPoint = ride.EndPoint,
                                OfferedById = availableRide.OfferedById,
                                Date = availableRide.Date,
                                TimeSlot = availableRide.TimeSlot,
                                Id = availableRide.Id,
                                Price = availableRide.Price,
                                AvailableSeats = GetAvailableSeats(availableRide.Stops, startPoint, pathLength),
                                Stops = GetUpdatedStops(availableRide.Stops, startPoint, pathLength),
                            };
                            if (newAvailableRide.AvailableSeats > 0)
                            {
                                availableRides.Add(newAvailableRide);
                            }
                        }
                    }
                }
                return availableRides;
            }
            catch(Exception ex)
            {
                return new List<CurrentRide>();
            }
        }

        public string GetLocationName(int locationId)
        {
            try
            {
                foreach (var location in CarpoolDb.Locations)
                {
                    if (location.Id == locationId)
                    {
                        return location.Name;
                    }
                }
                return "";
            }
            catch(Exception ex)
            {
                return "";
            }
        }

        public List<int> IsPathExists(Ride ride,string StartPoint,string Endpoint)
        {
            try
            {
                List<Stop> path = ride.Stops;
                List<int> result = new List<int>();

                for (int i = 0; i < path.Count ; i++)
                {
                    if (GetLocationName(path[i].LocationId) == StartPoint)
                    {
                        for (int j = 0; j < path.Count; j++)
                        {
                            if (GetLocationName(path[i + j].LocationId) == Endpoint)
                            {
                                result.Add(i);
                                result.Add(j+1);
                                return result;
                            }
                        }
                    }
                }
                result.Add(-1);
                return result;
            }
            catch(Exception ex)
            {
                List<int> result = new List<int>(); result.Add(0);
                return result;
            }
        }

        public bool BookTheRide(CurrentRide currentRide, int userId)
        {
            Console.WriteLine(currentRide.EndPoint);
            Console.WriteLine(currentRide.StartPoint);
            
            try
            {
                Ride availableRide = CarpoolDb.Rides.Include(r => r.Stops).Where(r => r.Id == currentRide.Id).First();

                if (availableRide != null)
                {
                    for (int i = 0; i < availableRide.Stops.Count; i++)
                    {
                        availableRide.Stops[i].SeatsAtStop = currentRide.Stops[i].SeatsAtStop;
                    }

                    
                    Ride newRide = new Ride()
                    {
                        
                        BookedById = userId,
                        IsBooked = true,
                        Price = currentRide.Price,
                        Date = currentRide.Date,
                        OfferedById = currentRide.OfferedById,
                        TimeSlot = currentRide.TimeSlot,
                        StartPoint=currentRide.StartPoint,
                        EndPoint = currentRide.EndPoint,
                
                    };


                    CarpoolDb.Rides.Add(newRide);
                    CarpoolDb.SaveChangesAsync();
                    return true;
                }
                return false;
            }

            catch(Exception ex)
            {
                return false;
            }

        }
    }

}