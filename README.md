# Financial Year Management Test Application

این پروژه یک برنامه ساده برای مدیریت سال‌های مالی است که شامل تعریف، ویرایش، و مشاهده اطلاعات سال مالی با استفاده از **ASP.NET Core 8 (MVC)** و **AG Grid** می‌باشد.

---

## امکانات پروژه

- **تعریف سال مالی**: امکان ثبت اطلاعات سال مالی شامل تاریخ شروع و پایان.
- **ویرایش سال مالی**: امکان ویرایش اطلاعات ثبت‌شده.
- **فیلتر و مرتب‌سازی**: استفاده از قابلیت‌های AG Grid برای فیلتر کردن و مرتب‌سازی اطلاعات.
- **اعتبارسنجی تاریخ‌ها**: جلوگیری از هم‌پوشانی تاریخ‌های شروع و پایان سال‌های مالی.
- **پشتیبانی از راست‌چین (RTL)**: مناسب برای زبان فارسی.
- **ارتباط با سرور**: مدیریت درخواست‌ها و عملیات CRUD با استفاده از Fetch API.

---

## تکنولوژی‌های استفاده‌شده

### Backend (سمت سرور)
- **ASP.NET Core 8 (MVC)**: برای مدیریت کنترلرها و عملیات سمت سرور.
- **Entity Framework Core 8**: برای تعامل با پایگاه داده.
- **SQL Server**: به عنوان پایگاه داده اصلی.
- **Fluent API**: برای تعریف اعتبارسنجی‌ها در سطح مدل‌ها.

### Frontend (سمت کلاینت)
- **AG Grid**: برای نمایش داده‌ها در جدول.
- **HTML5 و CSS3**: برای طراحی رابط کاربری.
- **JavaScript (ES6)**: برای مدیریت رویدادها و ارتباط با سرور.

---

## راه‌اندازی پروژه

### پیش‌نیازها
1. نصب **.NET SDK 8.0**.
2. نصب **SQL Server**.
3. مرورگر مدرن مانند **Google Chrome** یا **Microsoft Edge**.

---

### مراحل راه‌اندازی

1. **کلون کردن مخزن**
   ```bash
   git clone https://github.com/your-username/financial-year-management.git
   cd financial-year-management
