namespace Dwapi.Crs.SharedKernel.Enums
{
    public enum CargoType
    {
        Patient,
        Metrics,
        AppMetrics
    }

    public enum ManifestStatus
    {
        Staged,
        Processed
    }
    public enum EmrSetup
    {
        SingleFacility,
        MultiFacility,
        Community
    }

    public enum Area
    {
        Generating,
        Processing,
        Transmitting,
        ReTransmitting,
        Deduplicating,
        ReProcessing
    }
}
