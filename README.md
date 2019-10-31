# class-web

![홈](https://raw.githubusercontent.com/wsx9412/web/master/django/class-web-master/register.PNG?raw=true)

대학교에서 진행한 창업동아리의 활동으로 게임을 제작하여 최종적으로 Google PlayStore에 출시하여 서비스 중인 게임 "용병 랜덤 디펜스"의 제작과정, 기본적인 게임 내용은 랜덤으로 유닛을 뽑아서 몰려오는 적으로부터 거점을 지키는 게임으로 현재는 100라운드를 최종라운드로 하고있지만, 무한모드 또한 개발하여 배포 완료

---

> 창업동아리 프로젝트

- 제작 인원: 5명

> 개요

- 게임 명 : 용병 랜덤 디펜스
- 개발 툴 : Unity, Visual Studio 2017
- 개발 언어 : C#
- 플랫폼 : android
- Google PlayStore 서비스 유무 : 유

> 제작 기간
- 2019.02 ~ 2019.06

> 맡은 역할
- UI 디자인
- UI의 기능
- 유닛간의 조합기능
- 유닛 배치 및 이동
- 유닛 퀵슬롯 및 퀵슬롯 상의 유닛 정렬
- 터치와 관련된 기능
---

### 상세내용
![1](https://github.com/wsx9412/Game/blob/master/Picture/1.JPG?raw=true)
![2](https://github.com/wsx9412/Game/blob/master/Picture/2.JPG?raw=true)  

게임의 특성 상 유닛을 배치하고 회수하는 기능이 존재하여 회수하고 다시 배치할 때 그 유닛의 체력이 보이게 하는 것이 중요했습니다.
따라서 퀵슬롯창과 맵의 LayerMask를 다르게하여 아이콘을 선택할 때에는 drag and drop을 이용, 정보를 볼때에는 RayCast Point를 이용하여 터치한 위치의 레이어를 확인하여 유닛의 배치가능 여부와 사거리를 볼 수 있도록 하였습니다. 또한 RayCast Circle을 이용하여 유닛을 배치할 수 있는 공간 주변에 다른 유닛이 있을 경우 유닛이 배치되는 공간을 보여주는 타원을 붉은색으로 바꾸어 유닛이 겹쳐서 배치되지 않도록 하였습니다.  

![3](https://github.com/wsx9412/Game/blob/master/Picture/3.JPG?raw=true)  

다음은 유닛 각각의 능력치에 관한 내용입니다.  

![4](https://github.com/wsx9412/Game/blob/master/Picture/4.JPG?raw=true)  
![5](https://github.com/wsx9412/Game/blob/master/Picture/5.JPG?raw=true)  

유닛을 뽑으면 그 유닛에 관련된 능력치가 일정 범위 내에서 자동으로 결정되며 그 유닛의 능력치는 퀵슬롯의 아이콘에 위와 같이 객체로서 존재하게 됩니다. 유닛을 배치할 때 이 GameObject를 배치된 유닛에 통합하여 두고 회수할 때 다시 아이콘의 형태로 퀵슬롯에 전달되도록 하였습니다.  

![6](https://github.com/wsx9412/Game/blob/master/Picture/6.JPG?raw=true)  

조합의 경우에는 나중에 업데이트하기 쉽게 하기 위해서 게임을 시작하고 처음 조합창을 켤 때 데이터베이스에서 조합유닛에 대한 데이터를 Json의 형태로 획득하여 조합유닛 목록을 만드는 것을 목표로 하였고 현재 존재하는 조합목록으로는 단순히 유닛을 다음등급으로 업그레이드 하는 조합과 게임을 플레이 하다 보면 얻을 수 있는 유물을 이용하여 조합하거나 유물이 없을 경우에 조합이 가능한 상급 유닛으로 나뉘어졌고 각각의 조합은 필요한 재료의 최소등급과 최고등급이 존재합니다.  

![7](https://github.com/wsx9412/Game/blob/master/Picture/7.JPG?raw=true)  

궁수를 다음 등급으로 조합하기 위한 조합창입니다. 각 버튼의 기능으로는 먼저 등급에 따른 조합을 하기위해 재료의 등급을 선택하는 버튼입니다.  

![8](https://github.com/wsx9412/Game/blob/master/Picture/8.JPG?raw=true)  

각 등급간의 차이를 확실히 확인하기 위해 텍스트 색의 차이를 두도록 하였습니다.  
재료를 넣기 위한 버튼으로는 + 버튼을 이용한 수동방식과 자동채우기를 이용한 방식이 존재합니다.  

![9](https://github.com/wsx9412/Game/blob/master/Picture/9.JPG?raw=true)  

+ 버튼을 누를 경우 이런 식으로 현재 가지고 있는 유닛들의 정보가 나오는데 이때 유닛의 정보를 읽어올 때에는 현재 조합식에 등록되어있는 유닛의 종류, 등급을 이용하여 퀵슬롯에서 불러온 정보를 읽어오는 것입니다. 이 유닛을 클릭할 경우 회색으로 활성화가 되는데 이는 Toggle을 이용하여 구현했습니다, 그 상태에서 우측 하단의 확인 버튼을 누르면  

![10](https://github.com/wsx9412/Game/blob/master/Picture/10.JPG?raw=true)  

유닛이 들어가는 것을 확인 할 수 있습니다. 다음은 자동채우기 방식으로 수동채우기 방식과 똑같이 퀵슬롯에서 정보를 읽어오는 것 까지는 같만 조합 유닛에 필요한 수만큼의 정보를 가져오고 자동으로 조합목록에 추가해줍니다. 구현할 때 이 전에 사용한 조합데이터의 정보가 그대로 넘오는 문제가 있었지만, 조합창에서 유닛을 선택하기전에 초기화 해주는것으로 문제를 해결할 수 있었습니다.
