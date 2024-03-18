using System;
using System.Collections.Generic;

namespace Task7_OOO_DoctorMag.DbModels;

public partial class Pacient
{
    public int IdPacient { get; set; }

    public string PacientSurname { get; set; } = null!;

    public string PacientName { get; set; } = null!;

    public string PacientPatronymic { get; set; } = null!;

    public string PacientPhone { get; set; } = null!;

    public string PacientPolis { get; set; } = null!;

    public DateOnly PacientBirth { get; set; }

    public string? PacientEmail { get; set; }

    public virtual ICollection<Visit> Visits { get; set; } = new List<Visit>();
}
