# FieldForms.Maui-OfflineFirst

> **Offline-first dynamic forms for field inspections (tablet-first).**  
> Build forms from JSON at runtime, work fully offline with SQLite, and sync to a .NET Web API when connectivity returns.

---

## Table of Contents

- [Overview](#overview)
- [Key Features](#key-features)
- [Architecture](#architecture)
- [Tech Stack](#tech-stack)
- [Repository Structure](#repository-structure)
- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [1) Run the Backend API](#1-run-the-backend-api)
  - [2) Seed a Sample Schema](#2-seed-a-sample-schema)
  - [3) Run the MAUI App](#3-run-the-maui-app)
  - [4) Simulate Offline & Sync](#4-simulate-offline--sync)
- [Configuration](#configuration)
  - [Client App Settings](#client-app-settings)
  - [Backend App Settings](#backend-app-settings)
- [JSON Form Schema](#json-form-schema)
  - [Minimal Example](#minimal-example)
  - [Field Type → Control Mapping](#field-type--control-mapping)
- [Local SQLite Data Model](#local-sqlite-data-model)
- [Sync Protocol](#sync-protocol)
- [Backend API Contract](#backend-api-contract)
- [Dynamic Form Rendering (Client Snippet)](#dynamic-form-rendering-client-snippet)
- [External Data Sources (Pickers)](#external-data-sources-pickers)
- [Background Sync](#background-sync)
- [Validation, Conflicts, and Security](#validation-conflicts-and-security)
- [Testing Scenarios](#testing-scenarios)
- [Roadmap](#roadmap)
- [Contributing](#contributing)
- [License](#license)

---

## Overview

**Problem:** Field technicians work in areas with poor or no connectivity and currently use paper forms, causing delays and errors.  
**Solution:** A .NET MAUI tablet app that:
- Downloads **form schemas** (JSON) from a server.
- Renders **dynamic forms** at runtime.
- Stores **submissions offline** in SQLite.
- **Automatically syncs** to a backend when online, with conflict handling.

---

## Key Features

- **Dynamic UI Generation**: Build MAUI pages from schema JSON (Entry, Picker, CheckBox, DatePicker, RadioGroup, Photo, Signature, etc.).
- **Offline Storage**: `sqlite-net-pcl` for schemas, submissions, attachments, and a durable sync queue.
- **Resilient Sync Engine**: Background sync with batching, retries (exponential backoff), and **last-write-wins** by default (pluggable strategies).
- **Connectivity Awareness**: Uses `Connectivity` APIs to trigger sync on network changes.
- **API-Driven Pickers**: Optional external data sources (e.g., NHTSA vPIC) to populate picker items dynamically.
- **Security & Observability**: At-rest encryption options, HTTPS + Bearer auth, client telemetry hooks.


---

## Tech Stack

**Client:** .NET 8, .NET MAUI, CommunityToolkit.Mvvm, sqlite-net-pcl, Refit (API), Polly (resilience)  
**Backend:** ASP.NET Core (.NET 8), Controllers/Minimal APIs, EF Core, FluentValidation, JWT  
**Build/Run:** `dotnet` CLI, Android Emulator / iOS Simulator

---
