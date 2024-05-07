using OpenTelemetry;
using System.Diagnostics;

class MyExporter : BaseExporter<Activity>
{
    public override ExportResult Export(in Batch<Activity> batch)
    {
        using var scope = SuppressInstrumentationScope.Begin();

        foreach (var activity in batch)
        {
            Console.WriteLine($"Export: {activity.DisplayName}");
        }

        return ExportResult.Success;
    }
}