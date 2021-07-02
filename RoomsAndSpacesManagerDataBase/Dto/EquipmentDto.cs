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
    public class EquipmentDto
    {

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
        }


        [Key]
        public int Id { get; set; }
        public int Number { get; set; }
        public string ClassificationCode { get; set; }
        public string TypeName { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public bool Mandatory { get; set; }
        public string Discription { get; set; }

        public int RoomId { get; set; }

        public virtual RoomDto Room { get; set; }


        public override string ToString()
        {
            return Name;
        }
    }
}
