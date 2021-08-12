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

        public EquipmentDto(RoomEquipmentDto roomEquipment)
        {
            Number = roomEquipment.Number;
            ClassificationCode = roomEquipment.ClassificationCode;
            TypeName = roomEquipment.TypeName;
            Name = roomEquipment.Name;
            Count = roomEquipment.Count;
            Mandatory = roomEquipment.Mandatory;
            Currently = roomEquipment.Mandatory;
        }


        [Key]
        public int Id { get; set; }
        public int Number { get; set; }
        public string ClassificationCode { get; set; }
        public string TypeName { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public bool Mandatory { get => mandatory; set => Set(ref mandatory, value); }
        public bool Currently { get => currently; set => Set(ref currently, value); }
        public string Discription { get; set; }

        public int RoomId { get; set; }

        public virtual RoomDto Room { get; set; }


        public override string ToString()
        {
            return Name;
        }
    }
}
