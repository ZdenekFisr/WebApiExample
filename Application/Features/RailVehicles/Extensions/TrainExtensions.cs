using Application.Features.RailVehicles.Model;
using System.Text;

namespace Application.Features.RailVehicles.Extensions
{
    /// <summary>
    /// Extension methods for the train classes.
    /// </summary>
    public static class TrainExtensions
    {
        /// <summary>
        /// Generates a string representation of the train arrangement.
        /// </summary>
        /// <param name="train">The train data transfer object containing the list of train-vehicles.</param>
        /// <returns>A string representing the arrangement of the train.</returns>
        public static string GetArrangement(this TrainListModel train)
        {
            if (train.TrainVehicles is null)
                return string.Empty;

            StringBuilder arrangement = new();
            TrainVehicleOutputModel[] trainVehicles = [.. train.TrainVehicles];
            for (int i = 0; i < trainVehicles.Length; i++)
            {
                if (trainVehicles[i].IsActive)
                    arrangement
                        .Append('[');

                if (trainVehicles[i].VehicleCount > 1)
                    arrangement
                        .Append(trainVehicles[i].VehicleCount)
                        .Append('×');

                arrangement
                    .Append(trainVehicles[i].VehicleName);

                if (trainVehicles[i].IsActive)
                    arrangement
                        .Append(']');

                if (i < trainVehicles.Length - 1)
                    arrangement
                        .Append('+');
            }

            return arrangement.ToString();
        }
    }
}
