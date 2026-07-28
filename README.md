# Real Estate Media Platform

一个面向房地产摄影公司与地产经纪人的房源媒体协作平台。

平台希望打通以下工作流程：摄影公司创建房源项目，关联地产经纪人与联系人，管理照片、户型图和视频，并在内容准备完成后交付给经纪人。

## 项目目标

1. 摄影公司注册并登录平台。
2. 创建和维护房源项目。
3. 为房源关联地产经纪人及联系人。
4. 上传、选择和管理房源照片、户型图及视频。
5. 推进房源状态，例如 `Created`、`Pending` 和 `Delivered`。
6. 将整理完成的媒体资料交付给地产经纪人。

## 技术栈

### 前端

- React 19、TypeScript、Vite
- React Router
- Tailwind CSS
- Axios / Fetch API

### 后端

- ASP.NET Core Web API、.NET 10
- Entity Framework Core
- ASP.NET Core Identity
- SQL Server
- Swagger / OpenAPI

## 目录结构

```text
RealEstate/
├── backend/
│   ├── RealEstateMediaPlatform.sln
│   └── RealEstateMediaPlatform.API/
│       ├── Controllers/       # HTTP API
│       ├── Data/              # DbContext 与开发数据初始化
│       ├── DTOs/              # API 请求和响应模型
│       ├── Migrations/        # EF Core 数据库迁移
│       └── Models/            # 实体和枚举
└── frontend/
    └── src/
        ├── api/               # 前端 API 客户端
        ├── components/        # 可复用组件
        ├── pages/             # 页面与路由入口
        └── types/             # TypeScript 类型
```

## 当前进度

项目目前处于“前端原型接入真实后端 API”的阶段。

已经完成或基本完成：

- 前后端基础工程和页面路由。
- 用户、摄影公司、经纪人、房源、联系人和媒体资源的数据模型。
- EF Core 初始数据库迁移。
- 管理端房源列表及创建、编辑、软删除界面。
- 房源查询、创建、更新、软删除、恢复和状态更新 API。
- 房源联系人查询 API。
- 房源管理页面接入真实 API。
- 摄影上传、房源详情、经纪人工作台等页面原型。
- 开发环境自动迁移及示例数据初始化。

尚未完成或需要继续完善：

- 登录、注册和基于角色的权限控制尚未接入完整业务流程。
- 经纪人的创建、修改和删除功能尚未接入 API。
- 媒体文件目前没有真正上传到后端或对象存储。
- Floor Plan、Videography 和 Deliver to agent 仍以界面原型为主。
- 搜索、筛选、错误提示和加载状态需要完善。
- Property Dashboard 应改为通过房源 ID 请求数据，避免刷新后丢失路由状态。
- 需要整理 `Agent` 与 `CaseContact` 的领域模型和前端类型。
- 尚未建立自动化测试。

> 注意：仓库当前包含尚未提交的 API 联调代码。继续开发前请先运行 `git status`，不要直接丢弃这些修改。

## 本地开发要求

- .NET 10 SDK
- Node.js 和 npm
- SQL Server，可通过 `localhost:1433` 访问

数据库连接字符串位于 `backend/RealEstateMediaPlatform.API/appsettings.json`。不要将生产密码或其他密钥提交到 Git；后续建议通过 User Secrets 或环境变量管理敏感配置。

## 启动后端

```powershell
cd C:\Users\repo\RealEstate\backend
dotnet run --project RealEstateMediaPlatform.API
```

默认地址：

- API：<http://localhost:5166>
- Swagger：<http://localhost:5166/swagger>

应用启动时会自动执行待处理的 EF Core migrations，并写入开发示例数据。因此 SQL Server 必须已经启动，而且连接字符串必须有效。

## 启动前端

```powershell
cd C:\Users\repo\RealEstate\frontend
npm.cmd install
npm.cmd run dev
```

默认地址：<http://localhost:5173>

在部分 Windows PowerShell 环境中，执行 `npm` 可能被 Execution Policy 拦截，此时可以直接使用 `npm.cmd`。

## 开发示例账号

| 角色 | 邮箱 | 密码 |
| --- | --- | --- |
| Photography Company | `admin@recam.com` | `Password123!` |
| Agent | `agent.alex@recam.com` | `Password123!` |
| Agent | `agent.mia@recam.com` | `Password123!` |

这些账号只用于本地开发，不得用于生产环境。

## 构建和检查

```powershell
cd C:\Users\repo\RealEstate\backend
dotnet build RealEstateMediaPlatform.sln

cd C:\Users\repo\RealEstate\frontend
npm.cmd run build
npm.cmd run lint
```

最近一次项目盘点中，后端构建、前端生产构建和 ESLint 均已通过。

## 推荐的后续开发顺序

1. 手工验证并整理当前未提交的房源 API 联调代码。
2. 修复表单验证、错误反馈、API 配置和详情页刷新问题。
3. 完成“登录 → 创建房源 → 关联经纪人 → 上传媒体 → 交付”的最小业务闭环。
4. 实现 Agent、CaseContact 和 MediaAsset 的完整 API。
5. 添加后端集成测试和关键前端测试。
6. 最后完善搜索、筛选、权限、对象存储及部署配置。

## 重要开发约定

- 前端当前默认调用 `http://localhost:5166`，后续应改为环境变量。
- 删除房源采用软删除，通过 `IsDeleted` 标记数据。
- 数据库结构变化必须创建并提交 EF Core migration。
- 开发 seed 数据和固定密码不得用于生产环境。
- 提交前至少运行后端构建、前端构建和 ESLint。
