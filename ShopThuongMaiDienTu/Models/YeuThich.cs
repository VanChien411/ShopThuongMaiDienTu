﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace ShopThuongMaiDienTu.Models;

public partial class YeuThich
{
    public int MaYt { get; set; }

    public int? MaHh { get; set; }

    public string MaKh { get; set; }

    public DateTime? NgayChon { get; set; }

    public string MoTa { get; set; }

    public virtual HangHoa MaHhNavigation { get; set; }

    public virtual KhachHang MaKhNavigation { get; set; }
}