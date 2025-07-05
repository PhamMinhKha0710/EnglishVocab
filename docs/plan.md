# Kế hoạch dự án ứng dụng học từ vựng tiếng Anh - 3 tháng

## Tổng quan dự án

### Mục tiêu
Xây dựng ứng dụng học từ vựng tiếng Anh thụ động với flashcard cho người lười học

### Công nghệ sử dụng
- **Frontend**: React 18+ với TypeScript
- **Backend**: .NET 9 với Clean Architecture
- **Database**: SQL Server / PostgreSQL
- **Authentication**: JWT
- **Styling**: Tailwind CSS / Material-UI

---

## Cấu trúc thư mục dự án

```
EnglishVocabApp/
├── src/
│   ├── Backend/
│   │   ├── EnglishVocab.API/                    # Presentation Layer
│   │   │   ├── Controllers/
│   │   │   ├── Middleware/
│   │   │   ├── Filters/
│   │   │   ├── Extensions/
│   │   │   ├── Program.cs
│   │   │   └── appsettings.json
│   │   │
│   │   ├── EnglishVocab.Application/            # Application Layer
│   │   │   ├── Common/
│   │   │   │   ├── Interfaces/
│   │   │   │   ├── Models/
│   │   │   │   └── Exceptions/
│   │   │   ├── Features/
│   │   │   │   ├── Auth/
│   │   │   │   │   ├── Commands/
│   │   │   │   │   ├── Queries/
│   │   │   │   │   └── Handlers/
│   │   │   │   ├── Words/
│   │   │   │   ├── WordSets/
│   │   │   │   ├── StudySessions/
│   │   │   │   └── UserProgress/
│   │   │   ├── Services/
│   │   │   └── Mappings/
│   │   │
│   │   ├── EnglishVocab.Domain/                 # Domain Layer (Core)
│   │   │   ├── Entities/
│   │   │   │   ├── User.cs
│   │   │   │   ├── Word.cs
│   │   │   │   ├── WordSet.cs
│   │   │   │   ├── StudySession.cs
│   │   │   │   └── UserProgress.cs
│   │   │   ├── ValueObjects/
│   │   │   ├── Enums/
│   │   │   ├── Interfaces/
│   │   │   ├── Events/
│   │   │   └── Exceptions/
│   │   │
│   │   ├── EnglishVocab.Infrastructure/          # Infrastructure Layer
│   │   │   ├── Persistence/
│   │   │   │   ├── Configurations/
│   │   │   │   ├── Repositories/
│   │   │   │   ├── Migrations/
│   │   │   │   └── ApplicationDbContext.cs
│   │   │   ├── Services/
│   │   │   │   ├── EmailService.cs
│   │   │   │   ├── FileService.cs
│   │   │   │   └── TextToSpeechService.cs
│   │   │   ├── Identity/
│   │   │   └── External/
│   │   │
│   │   └── EnglishVocab.Tests/                  # Test Projects
│   │       ├── EnglishVocab.UnitTests/
│   │       ├── EnglishVocab.IntegrationTests/
│   │       └── EnglishVocab.E2ETests/
│   │
│   └── Frontend/                                # React Application
│       ├── public/
│       ├── src/
│       │   ├── components/
│       │   │   ├── common/
│       │   │   ├── auth/
│       │   │   ├── flashcard/
│       │   │   ├── words/
│       │   │   └── dashboard/
│       │   ├── pages/
│       │   ├── hooks/
│       │   ├── services/
│       │   ├── store/
│       │   ├── types/
│       │   ├── utils/
│       │   └── styles/
│       ├── package.json
│       └── tsconfig.json
│
├── docs/
├── scripts/
├── .github/
├── docker-compose.yml
├── README.md
└── .gitignore
```
```
Cấu trúc file chi tiết cho từng layer
1. Domain Layer (EnglishVocab.Domain)
EnglishVocab.Domain/
├── Entities/
│   ├── BaseEntity.cs
│   ├── User.cs
│   ├── Word.cs
│   ├── WordSet.cs
│   ├── StudySession.cs
│   └── UserProgress.cs
├── ValueObjects/
│   ├── Email.cs
│   ├── Password.cs
│   └── Pronunciation.cs
├── Enums/
│   ├── DifficultyLevel.cs
│   ├── MasteryLevel.cs
│   └── StudyMode.cs
├── Interfaces/
│   └── IRepository.cs
├── Events/
│   ├── WordLearnedEvent.cs
│   └── StudySessionCompletedEvent.cs
└── Exceptions/
    ├── DomainException.cs
    └── ValidationException.cs
2. Application Layer (EnglishVocab.Application)
EnglishVocab.Application/
├── Common/
│   ├── Interfaces/
│   │   ├── IApplicationDbContext.cs
│   │   ├── IEmailService.cs
│   │   └── IFileService.cs
│   ├── Models/
│   │   ├── Result.cs
│   │   ├── PaginatedList.cs
│   │   └── ApiResponse.cs
│   └── Exceptions/
├── Features/
│   ├── Auth/
│   │   ├── Commands/
│   │   │   ├── LoginCommand.cs
│   │   │   └── RegisterCommand.cs
│   │   ├── Queries/
│   │   │   └── GetUserProfileQuery.cs
│   │   └── Handlers/
│   ├── Words/
│   │   ├── Commands/
│   │   │   ├── CreateWordCommand.cs
│   │   │   └── UpdateWordCommand.cs
│   │   ├── Queries/
│   │   │   ├── GetWordsQuery.cs
│   │   │   └── SearchWordsQuery.cs
│   │   └── Handlers/
│   └── StudySessions/
├── Services/
│   ├── SpacedRepetitionService.cs
│   └── ProgressTrackingService.cs
└── Mappings/
    └── MappingProfile.cs
3. Infrastructure Layer (EnglishVocab.Infrastructure)
EnglishVocab.Infrastructure/
├── Persistence/
│   ├── Configurations/
│   │   ├── UserConfiguration.cs
│   │   ├── WordConfiguration.cs
│   │   └── WordSetConfiguration.cs
│   ├── Repositories/
│   │   ├── UserRepository.cs
│   │   ├── WordRepository.cs
│   │   └── WordSetRepository.cs
│   ├── Migrations/
│   └── ApplicationDbContext.cs
├── Services/
│   ├── EmailService.cs
│   ├── FileService.cs
│   └── TextToSpeechService.cs
├── Identity/
│   ├── JwtService.cs
│   └── IdentityService.cs
└── External/
    ├── TranslationService.cs
    └── DictionaryApiService.cs
4. API Layer (EnglishVocab.API)
EnglishVocab.API/
├── Controllers/
│   ├── AuthController.cs
│   ├── WordsController.cs
│   ├── WordSetsController.cs
│   └── StudySessionsController.cs
├── Middleware/
│   ├── ExceptionMiddleware.cs
│   └── LoggingMiddleware.cs
├── Filters/
│   └── ValidationFilter.cs
├── Extensions/
│   ├── ServiceCollectionExtensions.cs
│   └── ApplicationBuilderExtensions.cs
├── Program.cs
└── appsettings.json
Dependency Flow (Clean Architecture)
API Layer (Controllers, Middleware)
    ↓ depends on
Application Layer (Use Cases, Handlers)
    ↓ depends on
Domain Layer (Entities, Business Rules)
    ↑ dependency inversion
Infrastructure Layer (Database, External Services)
Packages cần thiết cho từng layer
Domain Layer

Không dependencies external (pure business logic)

Application Layer
xml<PackageReference Include="MediatR" Version="12.1.1" />
<PackageReference Include="AutoMapper" Version="12.0.1" />
<PackageReference Include="FluentValidation" Version="11.7.1" />
Infrastructure Layer
xml<PackageReference Include="Microsoft.EntityFrameworkCore" Version="8.0.0" />
<PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="8.0.0" />
<PackageReference Include="Microsoft.EntityFrameworkCore.Tools" Version="8.0.0" />
<PackageReference Include="Microsoft.AspNetCore.Identity.EntityFrameworkCore" Version="8.0.0" />
API Layer
xml<PackageReference Include="Microsoft.AspNetCore.Authentication.JwtBearer" Version="8.0.0" />
<PackageReference Include="Swashbuckle.AspNetCore" Version="6.5.0" />
<PackageReference Include="Serilog.AspNetCore" Version="7.0.0" />
```
---

## Tiến độ hiện tại (cập nhật: tháng 6/2025)

### Đã hoàn thành:

#### Backend
- [x] Thiết lập dự án .NET Clean Architecture
- [x] Thiết kế và cài đặt database schema
- [x] Migration cho Identity và main database
- [x] JWT Authentication (login, register, refresh token)
- [x] Logout API endpoint với token invalidation
- [x] User profile management API
- [x] Token expiration (15 phút) và automatic refresh
- [x] Error handling middleware

#### Frontend
- [x] Thiết lập dự án React với TypeScript
- [x] Authentication UI (login, register screens)
- [x] Protected routes và guest routes
- [x] User profile management UI
- [x] Context API cho quản lý trạng thái đăng nhập
- [x] Token interceptor cho xử lý token hết hạn
- [x] Remember me functionality

### To-do list cho phase tiếp theo (Word Management):

#### Backend Tasks
1. [ ] **Word Entity & DB Setup**
   - [ ] Cập nhật Word entity với các trường cần thiết
   - [ ] Tạo WordRepository interface và implementation
   - [ ] Thiết lập entity configuration cho Word

2. [ ] **WordSet Entity & DB Setup**
   - [ ] Cập nhật WordSet entity với các trường cần thiết
   - [ ] Tạo WordSetRepository interface và implementation
   - [ ] Thiết lập entity configuration cho WordSet
   - [ ] Xác định relationship giữa User-WordSet-Word

3. [ ] **Word API Endpoints**
   - [ ] CreateWord command và handler
   - [ ] UpdateWord command và handler
   - [ ] DeleteWord command và handler
   - [ ] GetWordById query và handler
   - [ ] GetWordsPaginated query và handler
   - [ ] SearchWords query và handler

4. [ ] **WordSet API Endpoints**
   - [ ] CreateWordSet command và handler
   - [ ] UpdateWordSet command và handler
   - [ ] DeleteWordSet command và handler
   - [ ] GetWordSetById query và handler
   - [ ] GetWordSetsPaginated query và handler
   - [ ] AddWordsToWordSet command và handler
   - [ ] RemoveWordsFromWordSet command và handler

5. [ ] **Import/Export Functionality**
   - [ ] ImportWordsFromCSV command và handler
   - [ ] ExportWordsToCSV query và handler
   - [ ] Validate và sanitize import data

#### Frontend Tasks
1. [ ] **Word Management Components**
   - [ ] WordList component
   - [ ] WordForm component (create/edit)
   - [ ] WordDetail component
   - [ ] WordFilter và SearchBar components

2. [ ] **WordSet Management Components**
   - [ ] WordSetList component
   - [ ] WordSetForm component (create/edit)
   - [ ] WordSetDetail component
   - [ ] WordSelector cho WordSet

3. [ ] **Import/Export UI**
   - [ ] CSV upload component
   - [ ] Import preview và validation
   - [ ] Export options UI

4. [ ] **State Management cho Word**
   - [ ] Word context/state
   - [ ] WordSet context/state
   - [ ] CRUD actions và reducers

### Tiếp theo:
Sau khi hoàn thành Word Management, chúng ta sẽ tiến hành:

1. Study System
   - Flashcard interface
   - Spaced repetition algorithm
   - Progress tracking

2. User Experience
   - Mobile responsive design
   - Dark mode
   - Offline functionality

---

## THÁNG 1: Thiết lập dự án và Backend Core

### Tuần 1: Thiết lập dự án và thiết kế
**Ngày 1-2: Thiết lập môi trường**
- Cài đặt .NET 9 SDK, Node.js, SQL Server
- Tạo repository GitHub
- Thiết lập CI/CD cơ bản

**Ngày 3-4: Thiết kế database**
- Thiết kế ERD cho:
  - Users (Id, Email, Password, CreatedAt, Settings)
  - WordSets (Id, Name, Description, UserId, IsPublic)
  - Words (Id, English, Vietnamese, Pronunciation, Example, Level)
  - UserProgress (Id, UserId, WordId, Mastery, LastReviewed)
  - StudySessions (Id, UserId, StartTime, EndTime, WordsStudied)

**Ngày 5-7: Thiết lập Clean Architecture Backend**
- Tạo các projects: API, Application, Domain, Infrastructure
- Cài đặt các packages cần thiết (EF Core, MediatR, AutoMapper)
- Thiết lập Domain Entities và Value Objects

### Tuần 2: Authentication và User Management ✓
**Ngày 8-10: Authentication ✓**
- Implement JWT authentication ✓
- User registration/login endpoints ✓
- Password hashing với BCrypt ✓
- Middleware authentication ✓

**Ngày 11-14: User Management ✓**
- User profile management ✓
- User settings (study preferences) ✓
- Password reset functionality ✓
- Input validation và error handling ✓

### Tuần 3: Word Management System
**Ngày 15-17: Word và WordSet entities**
- CRUD operations cho Words
- CRUD operations cho WordSets
- Import từ file CSV/Excel
- Seed data với 1000+ từ vựng phổ biến

**Ngày 18-21: Word Search và Filtering**
- Search từ vựng theo tiếng Anh/tiếng Việt
- Filter theo level, category
- Pagination
- API endpoints cho mobile-friendly

### Tuần 4: Progress Tracking
**Ngày 22-24: User Progress System**
- Tracking học tập của user
- Spaced repetition algorithm cơ bản
- Progress statistics
- Study streak tracking

**Ngày 25-28: Session Management**
- Study session recording
- Time tracking
- Daily/weekly/monthly reports
- Achievement system cơ bản

---

## THÁNG 2: Frontend Development và Integration

### Tuần 5: React Setup và Authentication
**Ngày 29-31: React Project Setup**
- Create React app với TypeScript
- Cài đặt routing (React Router)
- Thiết lập state management (Context API/Redux Toolkit)
- HTTP client setup (Axios)

**Ngày 32-35: Authentication UI**
- Login/Register pages
- Protected routes
- User profile page
- Password reset UI

### Tuần 6: Flashcard Core Features
**Ngày 36-38: Flashcard Component**
- Flashcard UI component với flip animation
- Keyboard navigation (Space, Enter, Arrow keys)
- Audio pronunciation (Text-to-Speech API)
- Responsive design

**Ngày 39-42: Study Session Interface**
- Study session page
- Progress indicators
- Know/Don't Know buttons
- Session summary

### Tuần 7: Word Management Interface
**Ngày 43-45: Word List Management**
- Word list display với search/filter
- Add/Edit/Delete words
- Import từ file CSV
- Bulk operations

**Ngày 46-49: WordSet Management**
- Create/Edit WordSets
- Share WordSets với users khác
- Browse public WordSets
- WordSet categories

### Tuần 8: Advanced Features
**Ngày 50-52: Spaced Repetition**
- Implement spaced repetition algorithm
- Due cards notification
- Study reminder system
- Smart card scheduling

**Ngày 53-56: Statistics và Progress**
- Dashboard với charts (Chart.js/Recharts)
- Learning progress visualization
- Study statistics
- Achievement badges

---

## THÁNG 3: Advanced Features và Deployment

### Tuần 9: Mobile Optimization và PWA
**Ngày 57-59: Mobile Experience**
- Responsive design optimization
- Touch gestures cho flashcards
- Mobile-first approach
- Offline capability cơ bản

**Ngày 60-63: PWA Features**
- Service worker setup
- App manifest
- Push notifications cho study reminders
- Install prompt

### Tuần 10: Advanced Learning Features
**Ngày 64-66: Adaptive Learning**
- Difficulty adjustment based on performance
- Personalized study recommendations
- Learning analytics
- Smart review scheduling

**Ngày 67-70: Content Enhancement**
- Example sentences
- Word pronunciation audio
- Images cho visual learning
- Word etymology/origin info

### Tuần 11: Testing và Performance
**Ngày 71-73: Testing**
- Unit tests cho backend (xUnit)
- Integration tests
- Frontend component tests (Jest, React Testing Library)
- E2E tests (Cypress)

**Ngày 74-77: Performance Optimization**
- Database query optimization
- Frontend bundle optimization
- Caching strategies
- Load testing

### Tuần 12: Deployment và Documentation
**Ngày 78-80: Deployment Setup**
- Docker containerization
- Azure/AWS deployment
- Database migration scripts
- Environment configuration

**Ngày 81-84: Documentation và Polish**
- API documentation (Swagger)
- User guide
- Code documentation
- UI/UX improvements

**Ngày 85-90: Launch Preparation**
- Beta testing
- Bug fixes
- Performance monitoring setup
- Launch checklist

---

## Tính năng chính theo độ ưu tiên

### Must-have (Tháng 1-2)
1. User authentication
2. Flashcard study system
3. Word management
4. Basic progress tracking
5. Spaced repetition

### Should-have (Tháng 2-3)
1. Statistics dashboard
2. Mobile optimization
3. Public word sets
4. Achievement system
5. Study reminders

### Could-have (Tháng 3)
1. Social features
2. Advanced analytics
3. Voice recognition
4. Gamification
5. Multi-language support

---

## Rủi ro và giải pháp

### Rủi ro kỹ thuật
- **Spaced repetition algorithm phức tạp**: Bắt đầu với thuật toán đơn giản, cải thiện dần
- **Performance với large dataset**: Implement pagination và lazy loading sớm
- **Mobile performance**: Test thường xuyên trên mobile devices

### Rủi ro thời gian
- **Scope creep**: Stick với MVP, note features cho future versions
- **Learning curve**: Dành thời gian học .NET 9 và React patterns
- **Integration issues**: Test integration sớm và thường xuyên

---

## Milestone và Deliverables

### Milestone 1 (Cuối tháng 1)
- Backend API hoàn thiện
- Database schema stable
- Authentication working
- Core word management

### Milestone 2 (Cuối tháng 2)
- Frontend MVP hoàn thiện
- Flashcard system working
- User can study và track progress
- Basic responsive design

### Milestone 3 (Cuối tháng 3)
- Production-ready application
- Deployed và accessible
- Documentation complete
- Ready for beta users

---

## Công cụ và Resources

### Development Tools
- Visual Studio 2022 / VS Code
- SQL Server Management Studio
- Swagger cho API testing
- Git với GitHub

### Learning Resources
- .NET 9 documentation
- React documentation
- Clean Architecture patterns
- Spaced repetition research papers

### Productivity Tools
- Trello/Jira cho task management
- Figma cho UI design
- Notion cho documentation

---

## Daily Routine Suggestion

### Weekdays (2-3 hours/day)
- 30 phút: Planning và review
- 1.5-2 giờ: Coding
- 30 phút: Testing và documentation

### Weekends (4-5 hours/day)
- 1 giờ: Review và planning
- 3-4 giờ: Feature development
- 30 phút: Testing và deployment

### Weekly Review
- Đánh giá progress
- Adjust timeline nếu cần
- Plan cho tuần tiếp theo
- Update documentation