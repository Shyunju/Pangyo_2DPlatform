![image](https://github.com/Shyunju/Pangyo_2DPlatform/assets/141755349/8e01161a-06dc-4ba4-baaa-c8c44633df9b)

<br>

> #### 플레이를 조작해 장애물과 몬스터를 피해 맵 목표 지점에 도달하는 게임 <br>
> 플레이어는 판교에 취업하기위해 각 스테이지에서 장애물과 몬스터를 피해 맵 끝에 위치한 취업에 필요한 아이템을 획득한다. 모든 스테이지의 아이템을 수집해 판교로 가보자.
> - 팀명 : B02조 판교상륙작전
> - 개발 기간 : 2023.10.12 ~ 2023.10.19
> - 사용 툴 : Unity(2022.3.2f1), Visual Studio, Figma

[팀 판교상륙작전 Figma](https://www.figma.com/file/FSKMeN1A37hGdWvXUeCuSU/%ED%8C%90%EA%B5%90%EC%83%81%EB%A5%99%EC%9E%91%EC%A0%84-team-library?type=design&node-id=2311-51&mode=design&t=9cwpOJgaoNnUljKb-0)
<br>
[팀 판교상륙작전 Notion](https://teamsparta.notion.site/02-2dc59acc3e6c4d88b12857a632f8222c)

<br><br>

# 시연 영상 :movie_camera:

[![스파르타 내일배움캠프 B02조 시연영상](https://img.youtube.com/vi/Onr1dKKhovQ/0.jpg)](https://www.youtube.com/watch?v=Onr1dKKhovQ "스파르타 내일배움캠프 B02조 시연영상")

<br><br><br><br><br>

# 씬 구성 :art:
### - Start Scene <br>
![image](https://github.com/Shyunju/Pangyo_2DPlatform/assets/141755349/7f517124-149b-4221-a272-09b5923173f9)

### - Stage Scene <br>
![image](https://github.com/Shyunju/Pangyo_2DPlatform/assets/141755349/332b0b69-c9c9-400b-9ec2-14b1cff4d2df)

### - Game Scene <br>
![image](https://github.com/Shyunju/Pangyo_2DPlatform/assets/141755349/e05b89a0-7ae6-4b97-a593-b69b2f5ca879)

### - End Scene <br>
![image](https://github.com/Shyunju/Pangyo_2DPlatform/assets/141755349/05f0b77f-8fc1-46c6-a580-77afe198269f)


<br><br><br><br><br>


# 역할 :video_game:
 ### :heart: 팀장 신현주
  - 스타트씬
  - 앤드씬 제작
  - 몬스터 구현
  - 일부 스프라이트 제작
  - 게임매니저 제작
  - UI 제작

<br>

 ### :orange_heart: 팀원 장현교
 - 아이템 제작
 - 사운드 디렉팅
 - 사운즈매니저 제작

<br>

 ### :yellow_heart: 팀원 이승연
 - 맵 디자인
 - 맵 제작
 - 레벨 디자인
 - UI 제작

<br>

### :green_heart: 팀원 우민규
- 플레이어 제작
- 플레이어 스탯 제작
- 발표자료 제작

<br>

-----
<br><br><br><br><br>

# 구현 사항 :white_check_mark:

### 프로젝트 주제 : 모험의 시작
[프로젝트 구현 내용 notion](https://teamsparta.notion.site/d7cbeb0ba2e441dfba25652b4da38606)
추가 구현 사항 1, 2, 3, 4, 6, 8, 9, 10, 11, 15
 *  **주인공 캐릭터의 이동 및 기본 동작**
    - 키보드 또는 터치 입력을 통해 주인공 캐릭터를 움직이고, 점프할 수 있어야 합니다.
    - 이동 및 점프 애니메이션을 포함하여 부드러운 이동 효과를 구현하세요.
  
 *  **레벨 디자인 및 적절한 게임 오브젝트 배치**
    - 최소한 2개 이상의 게임 레벨을 디자인하고 구현하세요.
    - 각 레벨에는 적절한 플랫폼, 장애물, 보상 아이템 등이 포함되어야 합니다.
  
 *  **충돌 처리 및 피해량 계산**
    - 주인공 캐릭터와 환경 또는 적 캐릭터 사이의 충돌을 처리하고, 피해량을 계산하여 게임 내에서 적절한 게임 오브젝트를 활용하세요.
  
 *  **UI/UX 요소**
    - 게임 시작 및 일시 정지 메뉴를 구현하세요.
    - 점수 표시, 생명력 게이지, 레벨 진행 상황 등 게임 상태를 나타내는 UI 요소를 추가하세요.
  
 *  **다양한 적 캐릭터와 그들의 행동 패턴 추가**
    - 여러 종류의 적 캐릭터를 도입하고, 각각 다른 행동 패턴과 전략을 가지도록 구현하세요.
  
 *  **다양한 무기나 아이템 추가**
    - 주인공 캐릭터에게 다양한 무기나 아이템을 도입하여 게임 플레이를 향상시키세요.
  
 *  **다양한 환경과 배경 설정**
    - 각 레벨에 다양한 환경과 배경을 추가하여 게임의 시각적 다양성을 확보하세요.
  
 *  **다양한 어려움 모드 또는 난이도 설정**
    - 플레이어가 게임 난이도를 선택할 수 있는 옵션을 추가하고, 다양한 어려움 모드를 구현하세요.
  
 *  **퀘스트 또는 미션 시스템 구현**
    - 게임 내에서 플레이어에게 주어지는 퀘스트 또는 미션 시스템을 추가하여 게임의 목표와 재미를 높이세요.
  
 *  **AI 적 캐릭터의 인공 지능 개선**
    - 적 캐릭터의 AI를 향상시켜, 더 스마트하게 플레이어를 추적하거나 공격할 수 있도록 구현하세요.
  
 *  **사운드 및 음악 효과 추가**
    - 게임에 배경 음악, 효과음, 대사 및 음성 오버레이를 추가하여 게임의 오디오 경험을 향상시키세요.
  
 *  **특수 효과 및 파티클 시스템 추가**
    - 게임에 다양한 특수 효과와 파티클 시스템을 도입하여 시각적 효과를 향상시키세요.
  
 *  **저장 및 불러오기 시스템 구현**
    - 게임 진행 상태를 저장하고 나중에 불러올 수 있는 저장 및 불러오기 시스템을 추가하세요.
  
 *  **게임 패턴 분석 및 밸런싱**
    - 플레이 테스트를 통해 게임 패턴을 분석하고 밸런싱을 조정하여 게임 난이도를 최적화하세요.

<br><br><br><br><br>

