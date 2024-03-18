using System;
using System.Collections.Generic;

namespace Task7_OOO_DoctorMag.DbModels;

public partial class Doctor
{
    public int IdDoctor { get; set; }

    public string DoctorSurname { get; set; } = null!;

    public string DoctorName { get; set; } = null!;

    public string DoctorPatronymic { get; set; } = null!;

    public int PositionIdPosition { get; set; }

    public string DoctorSchedule { get; set; } = null!;

    public int TypeVisitIdTypeVisit { get; set; }

    public int StatusIdStatus { get; set; }

    public string? DoctorPhoto { get; set; }

    public virtual Position PositionIdPositionNavigation { get; set; } = null!;

    public virtual Status StatusIdStatusNavigation { get; set; } = null!;

    public virtual TypeVisit TypeVisitIdTypeVisitNavigation { get; set; } = null!;

    public virtual ICollection<Visit> Visits { get; set; } = new List<Visit>();
}
