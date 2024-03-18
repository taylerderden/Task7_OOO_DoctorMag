using System;
using System.Collections.Generic;

namespace Task7_OOO_DoctorMag.DbModels;

public partial class Position
{
    public int IdPosition { get; set; }

    public string PositionName { get; set; } = null!;

    public virtual ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
