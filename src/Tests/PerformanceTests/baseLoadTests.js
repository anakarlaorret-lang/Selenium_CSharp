import http from 'k6/http';
import { sleep } from 'k6';

//Load test to simulate a base load on the White Tower careers page
export let options = {
  vus: 20,            // 20 concurrent users
  duration: '30s',    // run for 30 seconds
  thresholds: {
    http_req_duration: ['p(95)<800'], // 95% of requests < 800ms
    http_req_failed: ['rate<0.01'],   // <1% errors
  },
};

export default function () {
  http.get('https://towersoft.net/careers');
  sleep(1);
}