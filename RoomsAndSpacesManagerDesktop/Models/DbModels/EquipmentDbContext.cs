using RoomsAndSpacesManagerDataBase.Dto;
using RoomsAndSpacesManagerDataBase.Dto.RoomInfrastructure;
using RoomsAndSpacesManagerDesktop.Models.DbModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomsAndSpacesManagerDesktop.Models.DbModels
{
    class EquipmentDbContext : MainDbContext
    {
        public List<RoomEquipmentDto> GetAllEquipments()
        {
            return context.RaSM_RoomEquipments.ToList();
        }

        public List<RoomEquipmentDto> GetEquipments(RoomNameDto roomName)
        {
            return context.RaSM_RoomEquipments.Where(x => x.RoomNameId == roomName.Id).ToList();
        }


        public void AddNewEquipment(RoomNameDto roomName)
        {
            context.RaSM_RoomEquipments.Add(new RoomEquipmentDto() { RoomNameId = roomName.Id});
            context.SaveChanges();
        }

        public void AddNewEquipments(List<RoomEquipmentDto> equipments)
        {
            context.RaSM_RoomEquipments.AddRange(equipments);
            context.SaveChanges();
        }

        public void AddNewEquipments(List<EquipmentDto> equipments, RoomDto room)
        {
            context.RaSM_Equipments.RemoveRange(context.RaSM_Equipments.Where(x => x.RoomId == room.Id));
            context.RaSM_Equipments.AddRange(equipments);
            context.SaveChanges();
        }


        public void RemoveEquipment(RoomEquipmentDto equipment)
        {
            context.RaSM_RoomEquipments.Remove(equipment);
            context.SaveChanges();
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }










    }
}
