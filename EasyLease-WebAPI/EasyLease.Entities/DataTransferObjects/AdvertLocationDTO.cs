using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyLease.Entities.DataTransferObjects {
    public class AdvertLocationDTO {
        public AdvertLocationDTO() {
            District = new List<string>();
        }

        public string Region { get; set; }
        public List<string> District { get; set; }
    }
}