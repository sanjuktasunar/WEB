using System;
using FluentMigrator;

namespace web.Migrations
{
    [Migration(204, "alter view,add proc")]
    public class _104_add_alter_view_proc:Migration
    {
        public override void Down()
        {
            throw new NotImplementedException();
        }

        public override void Up()
        {
            string viewprocpath = System.Web.HttpContext.Current.Server.MapPath("/Query/103_alter_table.sql");
            Execute.Script(viewprocpath);
        }
    }
}