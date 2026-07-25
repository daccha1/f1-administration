using f1_administraiton_API.Models;

namespace f1_administraiton_API.Contracts
{
	public interface ISeatingZoneRepository
	{
		public Task<SeatingZone> CreateSeatingZone(SeatingZone newSeatingZone);
		public Task<SeatingZone> GetSeatingZoneById(int id);
		public Task<List<SeatingZone>> GetSeatingZones();
		public Task<bool> DeleteSeatingZone(int id);
		public Task<SeatingZone> UpdateSeatingZone(SeatingZone updatedSeatingZone);
	}
}
