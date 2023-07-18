using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace amontenegroS7
{
    public interface Database
    {
        SQLiteAsyncConnection  GetConnection();
    }
}
