# DB-based Feature Point Matching for Obstacle Recognition in AR Environment

### Description

  - 모바일 AR환경에서 사용자의 안전성 개선을 위한 장애물 감지 시스템이다. 
  - 조명에 관계없이 장애물을 효과적으로 탐지하기 위해 DB를 이용한다.
  - 장애물의 위치를 저장하여 조명이 충부하지 않을 경우 DB를 통해 장애물의 유무를 판단한다.

### Details
  - 조명이 충분한 경우 실시간 장애물 탐지를 통해 장애물의 GPS위치와 그때 사용자의 시점을 저장한다.
  - 노란색 화면은 조명이 충분한 경우 장애물을 찾는 과정이다.
  - 빨간색 화면은 조명이 충분하지 않은 경우로 사전에 장애물이 탐지되었다면 GPS 좌표를 이용하여 장애물의 유무를 판단한다.


![스크린샷 2022-01-06 오후 9 14 58](https://user-images.githubusercontent.com/48409306/161484444-399292b9-076a-410d-b0a2-97bc7f884275.png)


### Library / IDE

  - Unity3D
  - ARFoundation
  - Firebase / Firestore

