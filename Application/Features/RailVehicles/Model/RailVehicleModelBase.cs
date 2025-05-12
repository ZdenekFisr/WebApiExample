using Application.Common;
using Domain;
using Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Application.Features.RailVehicles.Model
{
    /// <summary>
    /// An abstract class that every rail vehicle model must inherit from.
    /// </summary>
    public abstract class RailVehicleModelBase : ModelBase
    {
        [StringLength(Constants.VehicleNameMaxLength, MinimumLength = Constants.VehicleNameMinLength)]
        public required string Name { get; set; }

        [StringLength(Constants.VehicleDescriptionMaxLength)]
        public required string Description { get; set; }

        /// <inheritdoc cref="RailVehicle.Weight"/>
        [Range(0, double.MaxValue, MinimumIsExclusive = true)]
        public double Weight { get; set; }

        /// <inheritdoc cref="RailVehicle.Length"/>
        [Range(0, double.MaxValue, MinimumIsExclusive = true)]
        public double Length { get; set; }

        /// <inheritdoc cref="RailVehicle.Wheelsets"/>
        [Range(0, byte.MaxValue, MinimumIsExclusive = true)]
        public byte Wheelsets { get; set; }

        /// <inheritdoc cref="RailVehicle.EquivalentRotatingWeight"/>
        [Range(0, double.MaxValue)]
        public double EquivalentRotatingWeight { get; set; }

        /// <inheritdoc cref="RailVehicle.MaxSpeed"/>
        [Range(0, short.MaxValue, MinimumIsExclusive = true)]
        public short MaxSpeed { get; set; }

        /// <inheritdoc cref="RailVehicle.ResistanceConstant"/>
        [Range(0, double.MaxValue)]
        public double ResistanceConstant { get; set; }

        /// <inheritdoc cref="RailVehicle.ResistanceLinear"/>
        [Range(0, double.MaxValue)]
        public double ResistanceLinear { get; set; }

        /// <inheritdoc cref="RailVehicle.ResistanceQuadratic"/>
        [Range(0, double.MaxValue)]
        public double ResistanceQuadratic { get; set; }

        public abstract RailVehicle ToEntity();
    }
}
