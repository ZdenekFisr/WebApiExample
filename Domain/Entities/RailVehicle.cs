﻿using Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class RailVehicle : EntityWithUserBase, ICreateHistory, IUpdateHistory, ISoftDeletable
    {
        [StringLength(Constants.VehicleNameMaxLength)]
        public required string Name { get; set; }

        public required string Description { get; set; }

        public double Weight { get; set; }

        public double Performance { get; set; }

        public short MaxSpeed { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public string? CreatedBy { get; set; }

        public DateTimeOffset? UpdatedAt { get; set; }

        public string? UpdatedBy { get; set; }

        public bool IsDeleted { get; set; }

        public DateTimeOffset? DeletedAt { get; set; }

        public string? DeletedBy { get; set; }
    }
}
