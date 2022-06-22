using System;
using AutoMapper;
using Dwapi.Crs.SharedKernel.Utils;

namespace Dwapi.Crs.Service.Application.Domain.Converters
{
    public class NameConverter : IValueConverter<string, string>
    {
        public string Convert(string source, ResolutionContext context) => source.Transfrom("Name").ToUpper();
    }

    public class SexConverter : IValueConverter<string, string>
    {
        public string Convert(string source, ResolutionContext context) => source.Transfrom("Sex").ToUpper();
    }

    public class MaritalConverter : IValueConverter<string, string>
    {
        public string Convert(string source, ResolutionContext context) => source.Transfrom("Marital").ToUpper();
    }

    public class PhoneConverter : IValueConverter<string, string>
    {
        public string Convert(string source, ResolutionContext context) => source.ToNumericFormat();
    }

    public class DateConverter : IValueConverter<DateTime?, string>
    {
        public string Convert(DateTime? source, ResolutionContext context) => source.ToDateFormat();
    }
}