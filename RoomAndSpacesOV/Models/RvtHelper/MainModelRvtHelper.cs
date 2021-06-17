using Autodesk.Revit.DB;
using RoomAndSpacesOV.Dto;
using RoomsAndSpacesManagerDataBase.Dto;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomAndSpacesOV.Models.RvtHelper
{
    class MainModelRvtHelper
    {
        public ParameterDto SetPropertt(string paramName, string dtoProp, Element item, RoomDto roomDto)
        {
            Parameter param = item.LookupParameter(paramName);
            if (param != null)
            {
                string storageType = param.StorageType.ToString();
                if (storageType.ToString() == "String")
                {
                    string propValue = roomDto.GetType().GetProperty(dtoProp)?.GetValue(roomDto)?.ToString();
                    if (param.AsString() != propValue)
                    {
                        ParameterDto parameterDto = new ParameterDto();
                        parameterDto.Name = paramName;
                        parameterDto.NewValue = propValue;
                        parameterDto.OldValue = param.AsString();
                        param?.Set(propValue);
                        return parameterDto;
                    }
                }

                if (storageType.ToString() == "Integer")
                {
                    string propValue = roomDto.GetType().GetProperty(dtoProp)?.GetValue(roomDto)?.ToString();
                    if (propValue != null & param.AsInteger() != int.Parse(propValue))
                    {
                        ParameterDto parameterDto = new ParameterDto();
                        parameterDto.Name = paramName;
                        parameterDto.NewValue = propValue.ToString();
                        parameterDto.OldValue = param.AsInteger().ToString();
                        param?.Set(int.Parse(propValue));
                        return parameterDto;
                    }
                }

                if (storageType.ToString() == "Double")
                {
                    double i;
                    string propValue = roomDto.GetType().GetProperty(dtoProp)?.GetValue(roomDto)?.ToString();
                    double.TryParse(propValue?.Replace('.', ','), out i);
                    if (param.AsDouble() != i)
                    {
                        ParameterDto parameterDto = new ParameterDto();
                        parameterDto.Name = paramName;
                        parameterDto.NewValue = i.ToString();
                        parameterDto.OldValue = param.AsDouble().ToString();
                        param?.Set(i);
                        return parameterDto;
                    }

                } 
            }
            return null;
        }
    }
}

