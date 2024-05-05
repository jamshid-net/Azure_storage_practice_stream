import { HttpClient } from '@angular/common/http';
import { ChangeDetectionStrategy, ChangeDetectorRef, Component, Inject, OnInit } from '@angular/core';
import { BehaviorSubject, Observable, Subject, Subscription, finalize, mergeMap, scan, takeUntil, tap } from 'rxjs';
import { JsonStreamDecoder } from './JsonStreamDecoder';

interface WeatherForecast {
  date: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class AppComponent implements OnInit {
  

  constructor(private http: HttpClient, private cdr: ChangeDetectorRef ) { }

  public forecasts : WeatherForecast[] = [];

  public forecasts$!: Observable<WeatherForecast[]>;
  public loading = true;
  private requestSubscribtion!: Subscription;
  private stopSource = new Subject();

  ngOnInit() {
    // this.forecasts$ = this.getForecastStream<WeatherForecast>
    // ('https://localhost:7085/weatherforecast')
    // .pipe(
    //   takeUntil(this.stopSource),
    //   scan((all, item) => [...all, item], [] as WeatherForecast[]),
    //   finalize(() => this.loading = false)
    // );
  }

 

  CancellReuqest() {
      this.stopSource.unsubscribe();
    }
  title = 'angularapp2.client';

    getForecastStream<T>(url: string)
    : Observable<T> 
    {
     return new Observable<T>(observer => 
      {
        const controller = new AbortController();
        fetch(url)
        .then(async response => {
          const reader = response.body?.getReader();
          if (!reader) {
            throw new Error('Failed to read response');
          }
          const decoder = new JsonStreamDecoder();
    
          while (true) {
            const { done, value } = await reader.read();
            if (done) break;
            if (!value) continue;
            const item = decoder.decodeChunk<T>(value,(parsedItem)=>{
              observer.next(parsedItem);
            });
      
          }
          reader.releaseLock();
        }).catch(err => observer.error(err));
        return () => controller.abort();
      }) 
     
    }


}
