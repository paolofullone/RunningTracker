import http from 'k6/http';
import { sleep, check, group } from 'k6';
import { htmlReport } from './Utils/bundle.js'
import { textSummary } from './Utils/index.js'

const testName = `${__ENV.TEST_NAME || 'LoadTest'}`;
const baseUrl = `${__ENV.BASE_URL || 'https://localhost:7119'}`;
const maxUsers = parseInt(__ENV.MAX_USERS) || 10;

export const options = {
    scenarios: {
        load_test: {
            executor: 'ramping-arrival-rate',
            startRate: 0,
            timeUnit: '1s',
            preAllocatedVUs: maxUsers,
            maxVUs: maxUsers * 2,
            stages: [
                { target: maxUsers, duration: '5s' },
                { target: 0, duration: '5s' },
            ],
        },
    },
};

export function handleSummary(data) {
    const now = new Date();
    const timestamp = now.toISOString().replace(/T/, '_').replace(/:/g, '-').split('.')[0];
    const htmlFileName = `summary_${testName}_${timestamp}.html`;

    return {
        [htmlFileName]: htmlReport(data, { title: `${testName} Report - ${timestamp}` }),
        'stdout': textSummary(data, { indent: ' ', enableColors: true }),
    };
}

export default function () {
    group('Load Test', function () {
        const res = http.get(`${baseUrl}/api/v1/run`);
        check(res, {
            'status is 200': (r) => r.status === 200,
            'response time < 200ms': (r) => r.timings.duration < 200,
        });
    });

    sleep(1);
}