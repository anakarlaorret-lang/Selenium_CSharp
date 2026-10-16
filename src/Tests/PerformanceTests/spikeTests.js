import http from 'k6/http';
import { sleep } from 'k6';

//Spike test to simulate a sudden spike in traffic on the White Tower careers page
export const options = {
  stages: [
    { duration: '30s', target: 10 },   // baseline: 10 users
    { duration: '10s', target: 200 },  // sudden spike to 200 users
    { duration: '30s', target: 200 },  // sustain spike load
    { duration: '10s', target: 10 },   // drop back to baseline
  ],
  thresholds: {
    http_req_duration: ['p(95)<800'],  // 95% of requests < 800ms
    http_req_failed: ['rate<0.01'],    // <1% error rate
  },
};

export default function () {
  const res = http.get('https://towersoft.net/careers'); // White Tower careers page
  sleep(1);
}