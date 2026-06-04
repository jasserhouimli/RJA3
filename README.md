# RJA3 — Lost & Found Item Matching API

Modular monolith API built with ASP.NET Core 10.0. Users report lost/found items; the system matches them using a scoring algorithm.

## Stack

.NET 10, PostgreSQL, JWT Bearer, ASP.NET Core Identity, EF Core, FluentValidation, Docker.

## Quick Start

```bash
dotnet restore && dotnet build
# Requires appsettings.json with ConnectionStrings:PostgreSQL, Jwt:Key/Issuer/Audience, apiSettings:api_version
cd RJA3
dotnet ef database update
dotnet run
```

Dev docs at `/scalar/v1` or `/swagger`.

## API

Prefix: `/api/v1` (configurable). Auth-protected endpoints require `Authorization: Bearer <token>`.

### Auth

| Method | Path | Body | Description |
|---|---|---|---|
| POST | `/auth/register` | `{ userName, email, password }` | Create account |
| POST | `/auth/login` | `{ email, password }` | Returns `{ accessToken, refreshToken, expiresAt }` |
| POST | `/auth/refresh-token` | `{ refreshToken }` | Rotate tokens |

### User

| Method | Path | Description |
|---|---|---|
| GET | `/api/users/me` | Current user profile |

### Lost Items

| Method | Path | Body/Query | Description |
|---|---|---|---|
| POST | `/lostitems/add` | `{ itemType, latitude, longitude, brand?, model?, color? }` | Report lost item |
| GET | `/lostitems` | `?pageNumber=1&pageSize=10` | Paginated list |
| GET | `/lostitems/{id}` | — | By ID |

### Found Items

| Method | Path | Body/Query | Description |
|---|---|---|---|
| POST | `/founditems/add` | `{ itemType, lat, lng, brand?, model?, color?, securityQuestions[] }` | Report found item |
| GET | `/founditems` | `?ItemType&Brand&Model&Color&PageNumber&PageSize` | Filtered list |
| GET | `/founditems/{id}` | — | By ID |
| GET | `/founditems/{id}/securityquestions` | — | Security Qs for claim |

### Matching

| Method | Path | Description |
|---|---|---|
| GET | `/items-matcher/lost-items/{lostItemId}/matches` | Returns scored matches against found items |

## Matching Algorithm

Compares lost vs found items of the same type. For phones: brand match (+20 points) + Haversine distance. Currently only phone matching is implemented.

## Modules

- **Auth** — Register, login, JWT + refresh tokens (ASP.NET Identity + PostgreSQL)
- **Users** — Profile creation via `UserRegisteredEvent`; GetMe endpoint
- **LostItems** — Report & query lost items (Phone, Document, Other types)
- **FoundItems** — Report found items with security questions for verification
- **ItemsMatcher** — Scoring engine that matches lost to found items

Each module has its own EF Core DbContext (bounded context). Cross-module communication uses an in-memory event bus.
