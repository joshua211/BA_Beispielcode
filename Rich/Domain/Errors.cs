using BeispielProjekt;
using ErrorOr;

namespace Beispielprojekt.Domain;

public static class Errors
{
    public static Error UnzureichendesBudget() =>
        Error.Validation(description: "Das vorhandene Budget reicht für diesen Vorgang nicht aus");

    public static Error FalscherGenehmigungsstatus(Status gewünschterStatus) => Error.Validation(
        description: $"Der Genehmigungsstatus befindet sich nicht im Status {gewünschterStatus.ToString()}");
}