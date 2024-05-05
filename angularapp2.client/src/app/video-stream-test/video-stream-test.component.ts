import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Observable, map, tap } from 'rxjs';

@Component({
  selector: 'app-video-stream-test',
  templateUrl: './video-stream-test.component.html',
  styleUrls: ['./video-stream-test.component.css'],
})
export class VideoStreamTestComponent implements OnInit {
  videoUrl : string = 'https://localhost:51032/api/MyBlobAzure/GetFile?id=';
  //videoUrl2 : string = 'http://127.0.0.1:10000/devstoreaccount1/videoes/0e2727b1-7a28-4f3a-a6cf-376155faff29';

  videoUrls!: Observable<string[]>;

  constructor(private http: HttpClient){}
  ngOnInit(): void
  {
    this.videoUrls = this.http.get<string[]>('https://localhost:51032/api/MyBlobAzure/GetAllFilesId')
                                .pipe(map(y => {
                                  return y.map(x => this.videoUrl + x);
                                }));
  }

  seekedHandler(event: any)
  {
    console.log(event);
  }
  
}
