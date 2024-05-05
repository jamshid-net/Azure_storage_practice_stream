import { Component } from '@angular/core';

@Component({
  selector: 'app-video-stream-test',
  templateUrl: './video-stream-test.component.html',
  styleUrls: ['./video-stream-test.component.css'],
})
export class VideoStreamTestComponent {
  videoUrl : string = 'https://localhost:51032/api/MyBlobAzure/GetFile?id=97c44841-0c5f-4e39-8690-06263439b8e6';
  //videoUrl2 : string = 'http://127.0.0.1:10000/devstoreaccount1/videoes/0e2727b1-7a28-4f3a-a6cf-376155faff29';
  
}
