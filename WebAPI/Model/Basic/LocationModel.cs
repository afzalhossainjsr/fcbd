using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Basic
{
    public class LocationModel
    {
        public string? Id{get;set;}
        public string? NameBn	{get;set;}
        public string? NameEn	{get;set;}
        public string? ParentId{get;set;}
        public string? Lat	{get;set;}
        public string? Lang{get;set;}
        public string? Type{get;set;} 
    }
}
