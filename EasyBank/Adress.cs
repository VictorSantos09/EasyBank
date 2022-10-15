﻿namespace EasyBank
{
    public class Adress
    {
        public string Country { get; set; } = "Brasil";
        public string City { get; set; }
        public string State { get; set; }
        public string Neiborhood { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string? HouseComplement { get; set; }
        public string FullAdress { get; set; }
    }
}