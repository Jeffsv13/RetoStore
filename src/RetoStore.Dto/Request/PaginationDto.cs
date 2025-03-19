﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetoStore.Dto.Request;

public class PaginationDto
{
    public int Page { get; set; } = 1;
    private int recordsPerPage { get; set; } = 10;
    public int RecordsPerPage
    {
        get
        {
            return recordsPerPage;
        }
        set
        {
            recordsPerPage = (value > maxRecordsPerPage) ? maxRecordsPerPage : value;
        }
    }

    private readonly int maxRecordsPerPage = 50;
}
