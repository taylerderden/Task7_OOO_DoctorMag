using System;
using System.Collections.Generic;

namespace Task7_OOO_DoctorMag.DbModels;

public partial class Visit
{
    public int IdVisit { get; set; }

    public int PacientIdPacient { get; set; }

    public int DoctorIdDoctor { get; set; }

    public DateOnly? VisitDateRecording { get; set; }

    public DateOnly? VisitDate { get; set; }

    public string? VisitTime { get; set; }

    public int TypeVisitIdTypeVisit { get; set; }

    public virtual Doctor DoctorIdDoctorNavigation { get; set; } = null!;

    public virtual Pacient PacientIdPacientNavigation { get; set; } = null!;

    public virtual TypeVisit TypeVisitIdTypeVisitNavigation { get; set; } = null!;
}
