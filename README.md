🏥 CEHRMS

Centralized Electronic Health Record Management System

CEHRMS is a centralized, web-based Electronic Health Record (EHR) management system designed to streamline healthcare workflows. It enables secure management of patient records, appointments, and user roles while ensuring efficient communication between healthcare stakeholders.

📌 Features

Centralized storage of electronic health records

Role-based access control

Secure patient data handling

QR Code integration for quick record access

Responsive and user-friendly interface

👥 System Modules
👤 Patient

View personal medical records

Access prescriptions and reports

Scan QR code for quick identification

👨‍⚕️ Doctor

View and update patient health records

Add diagnoses and prescriptions

Track patient medical history

🧾 Receptionist

Register new patients

Schedule and manage appointments

Generate patient QR codes

🛠 Admin

Manage users (Doctors, Receptionists, Patients)

Monitor system activity

Maintain overall system configuration

🛠 Technologies Used

Backend: .NET Framework

Frontend: HTML, CSS, Bootstrap

Database: SQL Server

QR Code Library: ZXing.Net

📦 NuGet Package Used

To enable QR code scanning and generation, the following NuGet package is used:

Install-Package ZXing.Net -Version 0.16.10


Open the project in Visual Studio

Restore NuGet packages:

Update-Package -reinstall


Configure the database connection string in Web.config

Run the project using IIS Express

🔐 Security

Role-based authentication and authorization

Secure handling of sensitive medical data

Restricted access based on user roles

📈 Future Enhancements

Integration with mobile applications

Cloud-based data storage

Advanced analytics and reporting

Appointment reminders via SMS/Email

📄 License

This project is developed for academic purposes.
