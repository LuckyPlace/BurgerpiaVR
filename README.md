# BurgerpiaVR

이 프로젝트는 전북대학교 대학생 2명이 협력하여 약 3개월 동안 Unity를 기반하여 개발한 게임입니다.
스테이지가 적용된 요리 타이쿤 VR 게임입니다. 제한 시간 내에 손님 주문을 해결하고 일정 횟수 이상 실패 시 게임이 끝납니다.

## 주요 기능

### 1. 재료 관리 및 조합
- **재료 복제 시스템** (`Duplicate_Ingredient.cs`): 일정 시간이 지나고 충돌이 없으면 재료를 복제하여 게임 내에 새로 배치.
- **재료 절단 기능** (`Cutting_Foods.cs`): 치즈나 생선 같은 재료를 절단하여 새로운 재료를 생성.
- **재료 쌓기 및 정렬** (`Stacker.cs`, `Stack_Switch.cs`): 재료를 정렬하고 쌓아 햄버거를 조립.

### 2. 요리 및 소스 추가
- **그릴 시스템** (`Grill_Ing.cs`, `Grillable.cs`): 재료를 조리하여 익힌 상태로 변환.
- **소스 추가 기능** (`Pouring_Sauce.cs`, `Saucable.cs`): 재료에 소스를 추가하여 완성된 상태로 변경.

### 3. 게임 관리
- **싱글톤 게임 매니저** (`GameManager.cs`): 게임 상태, 손님 관리, 재료 데이터 초기화를 포함한 주요 게임 로직 제어.
- **생명 표시 시스템** (`Life_Indicator.cs`): 손님 주문 실패 시 생명을 차감하고 게임 오버 상태를 관리.

### 4. 손님 및 주문 관리
- **손님 이동 및 대기** (`Move_Guest.cs`, `Move_Guest_Renewal.cs`): 손님이 지정된 경로를 따라 이동하고 빈 카운터를 찾아 대기.
- **주문 생성 및 확인** (`Make_Order.cs`, `Serve_Menu.cs`, `Show_Menu.cs`): 손님의 주문을 생성하고, 사용자가 만든 햄버거와 비교하여 확인.

### 5. 환경 상호작용
- **문 개폐 기능** (`DoorOpener.cs`, `DoorSensor.cs`): 손님이 다가올 때 문을 자동으로 여닫음.
- **접시 정렬** (`Plate_Align.cs`): 재료를 접시에 중앙 정렬.
- **소스 병 재배치** (`Sauce_Bottle_Respawn.cs`): 소스 병이 지정된 위치를 벗어나면 원래 위치로 복귀.

## 프로젝트 구조

### 주요 스크립트
- **재료 관리**:
  - `Duplicate_Ingredient.cs`: 재료 복제.
  - `Cutting_Foods.cs`: 재료 절단.
  - `Stacker.cs`, `Stack_Switch.cs`: 재료 정렬 및 쌓기.
- **요리 및 소스**:
  - `Grill_Ing.cs`: 재료 조리.
  - `Pouring_Sauce.cs`, `Saucable.cs`: 소스 추가.
- **게임 로직**:
  - `GameManager.cs`: 게임 상태 및 주요 로직 관리.
  - `Life_Indicator.cs`: 생명 시스템.
- **손님 관리**:
  - `Move_Guest.cs`, `Move_Guest_Renewal.cs`: 손님 이동.
  - `Make_Order.cs`: 손님 주문 생성.
  - `Serve_Menu.cs`, `Show_Menu.cs`: 주문 확인 및 메뉴 표시.
- **환경 상호작용**:
  - `DoorOpener.cs`, `DoorSensor.cs`: 문 개폐.
  - `Plate_Align.cs`: 접시 정렬.
  - `Sauce_Bottle_Respawn.cs`: 소스 병 복귀.
