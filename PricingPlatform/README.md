Pricing Platform API

ระบบ API สำหรับคำนวณราคา (Pricing Engine) ที่ออกแบบมาให้ยืดหยุ่น รองรับการคำนวณราคาตามกฎทางธุรกิจ (Business Rules)

ภาพรวมสถาปัตยกรรม (Architecture Overview)

โปรเจกต์นี้ออกแบบด้วยแนวคิด Clean Architecture แยกหน้าที่ชัดเจนเป็น 4 Layer หลัก:

🔹 1. Domain Layer (Pricing.Domain)

เป็นแกนหลักของระบบ ใช้เก็บโครงสร้างข้อมูลและ interface ต่างๆ เช่น:

QuoteRequest, QuoteResponse
JobModel
PricingRule
Repository Interfaces (IJobRepository, IRuleRepository)


2. Application Layer (Pricing.Application)

เป็นส่วนของ business logic ทั้งหมด เช่น:

PricingService → ตัวหลักในการคำนวณราคา
Pricing Rules เช่น:
Weight Rule (คิดตามน้ำหนัก)
Remote Rule (พื้นที่ห่างไกล)
Time Rule (งานเร่งด่วน)

ทำหน้าที่ประมวลผลตามกฎต่างๆ

🔹 3. Infrastructure Layer (Pricing.Infrastructure)

ดูแลเรื่องการจัดเก็บข้อมูล เช่น:

InMemoryJobRepository
InMemoryRuleRepository

🔹 4. API Layer (Pricing.API)

เป็นส่วนที่เปิดให้ client เรียกใช้งานผ่าน HTTP เช่น:

QuotesController → คำนวณราคา
JobsController → เช็คสถานะงาน
RulesController → จัดการ rules
HealthController → ตรวจสอบสถานะระบบ

วิธีติดตั้งและใช้งาน (Setup Guide)
สิ่งที่ต้องมี
.NET 8 SDK
Docker (optional)
Git
🔹 1. Clone โปรเจกต์
git clone <https://github.com/Zeronic1559/ZeabixAPITest.git>
cd PricingPlatform
🔹 2. ติดตั้ง dependencies
dotnet restore
🔹 3. Build โปรเจกต์
dotnet build
🔹 4. Run Test
dotnet test
🔹 5. รัน API
dotnet run --project src/Pricing.API

เข้าใช้งานได้ที่:

API: http://localhost:5000
Swagger: http://localhost:5000/swagger
 รันด้วย Docker (ทางเลือก)
docker-compose up --build
 ตัวอย่างการใช้งาน (Sample Requests)
 1. ตรวจสอบสถานะระบบ (Health Check)
GET /health

Response

{
  "status": "healthy"
}
🔹 2. คำนวณราคาสินค้า (Single Quote)
POST /quotes/price

Request

{
  "basePrice": 100,
  "weight": 5.5,
  "location": "remote",
  "destination": "International"
}

Response

{
  "basePrice": 100,
  "finalPrice": 127.5,
  "appliedRules": [
    "Weight Rule: +10%",
    "Remote Location: +15%"
  ]
}
🔹 3. คำนวณหลายรายการ (Bulk)
POST /quotes/bulk

Response

{
  "job_id": "xxxx-xxxx"
}
🔹 4. ตรวจสอบสถานะงาน
GET /jobs/{jobId}