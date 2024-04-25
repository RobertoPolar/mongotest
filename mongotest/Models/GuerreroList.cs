using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Net;
using System.Reflection;

namespace mongotest.Models
{
    public class GuerreroList
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public int Idade { get; set; }
        public int Armas { get; set; }
        public string Atributo { get; set; }
        public int Ataque { get; set; }
        public int Exp { get; set; }

        public GuerreroList(Guerrero guerrero)
        {
            DateTime idadeCalculada = DateTime.Parse(guerrero.Birthday);
            var equippedWeapon = guerrero.Weapons.Where(x => x.Equipped == true).FirstOrDefault();
           
            TryGetPropertyValue<int, Attribute>(guerrero.Attributes, "strength", out var value);

            int mod = value switch
            {
                > 0 and <= 8 => -2,
                >= 9 and <= 10 => -1,
                >= 11 and <= 12 => 0,
                >= 13 and <= 15 => 1,
                >= 16 and <= 18 => 2,
                >= 19 and <= 20 => 3,
                _ => 0
            };

            this.Id = guerrero.Id;
            this.Nome = guerrero.Name;
            this.Idade = DateTime.Today.Year - idadeCalculada.Year;
            this.Armas = guerrero.Weapons.Count;
            this.Atributo = guerrero.KeyAttribute;
            this.Ataque = 10 + mod + (equippedWeapon == null ? 0 : equippedWeapon.Mod);
            this.Exp = (int)Math.Floor((this.Idade - 7) * Math.Pow(22, 1.45));
            
        }

        public static bool TryGetPropertyValue<TType, TObj>(TObj obj, string propertyName, out TType? value)
        {
            value = default;
            if (obj is null)
            {
                return false;
            }
            PropertyInfo? propertyInfo = typeof(TObj).GetProperty(propertyName);
            if (propertyInfo is null)
            {
                return false;
            }
            object? propertyValue = propertyInfo.GetValue(obj);
            if (propertyValue is null && Nullable.GetUnderlyingType(typeof(TType)) is not null)
            {
                return true;
            }
            if (propertyValue is not TType typedValue)
            {
                return false;
            }
            value = typedValue;
            return true;
        }
    }
}
