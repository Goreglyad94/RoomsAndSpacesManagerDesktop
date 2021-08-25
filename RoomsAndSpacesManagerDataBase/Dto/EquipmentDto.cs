using RoomsAndSpacesManagerDataBase.Dto.RoomInfrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomsAndSpacesManagerDataBase.Dto
{
    [Table("RaSM_Equipments")]
    public class EquipmentDto : ViewModel
    {
        private bool mandatory;
        private bool currently;


        public EquipmentDto()
        {

        }

        public EquipmentDto(RoomEquipmentDto roomEquipment, int? SubdivisionForse)
        {
            Number = roomEquipment.Number;
            ClassificationCode = roomEquipment.ClassificationCode;
            TypeName = roomEquipment.TypeName;
            Name = roomEquipment.Name;
            Count = roomEquipment.Count;
            Mandatory = roomEquipment.Mandatory;
            Currently = roomEquipment.Mandatory;
            CalcCount = roomEquipment.CalcCount;
        }

        public EquipmentDto(EquipmentDto equipmentDto, int roomId, int? SubdivisionForse)
        {
            Number = equipmentDto.Number;
            ClassificationCode = equipmentDto.ClassificationCode;
            TypeName = equipmentDto.TypeName;
            Name = equipmentDto.Name;
            Count = equipmentDto.Count;
            Mandatory = equipmentDto.Mandatory;
            Currently = equipmentDto.Currently;
            Discription = equipmentDto.Discription;
            RoomId = roomId;
            CalcCount = equipmentDto.CalcCount;
        }

        [Key]
        public int Id { get; set; }
        public int Number { get; set; }
        public string ClassificationCode { get; set; }
        public string TypeName { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public string CalcCount { get; set; }

        public bool Mandatory { get => mandatory; set => Set(ref mandatory, value); }
        public bool Currently { get => currently; set => Set(ref currently, value); }
        public string Discription { get; set; }

        private int roomId;
        public int RoomId 
        { 
            get => roomId;
            set => roomId = value; 
        }

        private RoomDto room;
        

        public virtual RoomDto Room
        {
            get => room;
            set => room = value;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}