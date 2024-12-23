using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NazariTest.Application.Enums
{
    public enum MessageType
    {
        [Description("ثبت با موفقیت انجام گردید")]
        InsertOk = 0,
        [Description("رخداد خطا در ثبت اطلاعات")]
        InsertFaile = 1,

        [Description("ویرایش با موفقیت انجام گردید")]
        UpdateOk = 2,
        [Description("رخداد خطا در ویرایش اطلاعات")]
        UpdateFaile = 3,

        [Description("حذف با موفقیت انجام گردید")]
        DeleteOk = 4,
        [Description("رخداد خطا در حذف اطلاعات")]
        DeleteFaile = 5,
        [Description("اطلاعاتی با این مشخصات یافت نشد")]
        RecordNotFound = 6,
        [Description("اطلاعات را صحیح وارد نمایید")]
        BadData = 7
    }
}
