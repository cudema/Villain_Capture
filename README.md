# 🎬 Villain Capture (빌런 캡처)

> **카메라 촬영 시스템과 턴제 전투 메커니즘을 결합한 유니티 게임 프로젝트**

## 📌 프로젝트 개요
- **개발 기간**: 
- **사용 엔진 및 언어**: Unity 2022.x / C#
- **담당 역할**: 메인 시스템 프로그래밍 (전투 FSM, 데이터 파싱, UI/카메라 로직 등)

---

## 🛠️ 핵심 아키텍처 및 시스템 구현

### 1. FSM 기반 턴제 전투 시스템 (Turn System)
- `BattleManager`와 `TurnFSM`을 통해 전투 상태(플레이어 턴, 적 턴, 결과 처리 등)를 모듈화하여 관리.
- 상태 패턴을 적용하여 각 턴 간의 결합도를 낮추고 이벤트를 통한 UI 동기화 구현.

### 2. 다형성(Polymorphism)을 활용한 적 및 패턴 확장 구조
- `EnemyBase` 추상 클래스 및 `IHealthReporter` 인터페이스 정의.
- 상속 체계를 통해 일반 적(`SampleEnemy`)과 보스(`DustyVeil`, `Ruby`)의 공격 패턴(`PatternBase`)을 손쉽게 추가할 수 있는 구조 설계.

### 3. Data-Driven 대화 및 아이템 파싱 (CSV Loader)
- `UICSVLoader`를 제작하여 기획 데이터를 CSV 형태로 분리.
- 스크립트 수정 없이 데이터 파일 변경만으로 대화 및 아이템 데이터 업데이트 가능하도록 구현.

### 4. 차별화된 플레이 메커니즘 (Filming & Post-Processing)
- `Filming.cs`를 통한 렌더링/카메라 인터랙션 구현.
- `MultiVignetteRendererFeature` 등 URP Post-Processing 커스텀 볼륨과 연동하여 시각적 연출 강화.

---

## 📂 폴더 구조 (Folder Structure)
Assets/Scripts/
├── Enemy/       # 적 베이스 클래스 및 패턴, 보스 로직
├── Player/      # 플레이어 컨트롤러 및 촬영(Filming) 메커니즘
├── Turn/        # 턴제 전투 FSM 상태 머신
├── LoadFile/    # CSV/데이터 파서 및 데이터 로딩 로직
├── Item/        # 아이템 데이터 및 인벤토리 구조
└── UI/          # UI 애니메이션 및 대화 출력 제어
