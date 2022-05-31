﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wachman.Tests.TimeTrackingServiceTests.Mocks
{
    internal static class MockResponses
    {
        internal static string EntriesResponse =>
        @"{
            'id': 144945783,
            'duration': '3626',
            'user_id': '1043590',
            'user_name': 'jode@doe.com',
            'task_id': '68568929',
            'task_note': '',
            'last_modify': '2022-05-26 07:01:55',
            'date': '2022-05-26',
            'start_time': '05:19:34',
            'end_time': '06:20:00',
            'locked': '0',
            'name': 'Wachman',
            'addons_external_id': '',
            'billable': 1,
            'invoiceId': '0',
            'color': '#42826E',
            'description': ''},
          {
            'id': 144950114,
            'duration': '3311',
            'user_id': '1043590',
            'user_name': 'jode@doe.com',
            'task_id': '19427046',
            'task_note': '',
            'last_modify': '2022-05-26 08:28:36',
            'date': '2022-05-26',
            'start_time': '07:01:56',
            'end_time': '07:57:07',
            'locked': '0',
            'name': 'Prywatne',
            'addons_external_id': '',
            'billable': 1,
            'invoiceId': '0',
            'color': '#F6B042',
            'description': 'Demo for the client'
          },
          {
            'id': 144958423,
            'duration': '1266',
            'user_id': '1043590',
            'user_name': 'jode@doe.com',
            'task_id': '92355524',
            'task_note': '',
            'last_modify': '2022-05-26 09:15:39',
            'date': '2022-05-26',
            'start_time': '08:54:33',
            'end_time': '09:15:39',
            'locked': '0',
            'name': 'SoftwareDevelopment',
            'addons_external_id': '',
            'billable': 1,
            'invoiceId': '0',
            'color': '#E9A6B7',
            'description': 'ID-007 Checking if tower passing'
          },
          {
            'id': 144960630,
            'duration': '559',
            'user_id': '1043590',
            'user_name': 'jode@doe.com',
            'task_id': '92355534',
            'task_note': '',
            'last_modify': '2022-05-26 09:49:04',
            'date': '2022-05-26',
            'start_time': '09:15:41',
            'end_time': '09:25:00',
            'locked': '0',
            'name': 'Meetings',
            'addons_external_id': '',
            'billable': 1,
            'invoiceId': '0',
            'color': '#E9A6B7',
            'description': 'Daily'
          },
          {
            'id': 144963768,
            'duration': '7446',
            'user_id': '1043590',
            'user_name': 'jode@doe.com',
            'task_id': '92355524',
            'task_note': '',
            'last_modify': '2022-05-26 11:29:06',
            'date': '2022-05-26',
            'start_time': '09:25:00',
            'end_time': '11:29:06',
            'locked': '0',
            'name': 'SoftwareDevelopment',
            'addons_external_id': '',
            'billable': 1,
            'invoiceId': '0',
            'color': '#E9A6B7',
            'description': 'ID-007 Pair programming with duck'
          },
          {
            'id': 144972812,
            'duration': '2574',
            'user_id': '1043590',
            'user_name': 'jode@doe.com',
            'task_id': '92355534',
            'task_note': '',
            'last_modify': '2022-05-26 13:30:46',
            'date': '2022-05-26',
            'start_time': '11:29:06',
            'end_time': '12:12:00',
            'locked': '0',
            'name': 'Meetings',
            'addons_external_id': '',
            'billable': 1,
            'invoiceId': '0',
            'color': '#E9A6B7',
            'description': 'Call with Andrzej about problems with selected cell binding'
          },
          {
            'id': 144979955,
            'duration': '2280',
            'user_id': '1043590',
            'user_name': 'jode@doe.com',
            'task_id': '92355524',
            'task_note': '',
            'last_modify': '2022-05-26 13:01:23',
            'date': '2022-05-26',
            'start_time': '12:12:00',
            'end_time': '12:50:00',
            'locked': '0',
            'name': 'SoftwareDevelopment',
            'addons_external_id': '',
            'billable': 1,
            'invoiceId': '0',
            'color': '#E9A6B7',
            'description': 'ID-007 Pair programming with duck'
          },
          {
            'id': 144980725,
            'duration': '2471',
            'user_id': '1043590',
            'user_name': 'jode@doe.com',
            'task_id': '92355534',
            'task_note': '',
            'last_modify': '2022-05-26 13:31:11',
            'date': '2022-05-26',
            'start_time': '12:50:00',
            'end_time': '13:31:11',
            'locked': '0',
            'name': 'Meetings',
            'addons_external_id': '',
            'billable': 1,
            'invoiceId': '0',
            'color': '#E9A6B7',
            'description': 'Call to duck about setting up radio buttons to work together'
          },
          {
            'id': 144983538,
            'duration': '6943',
            'user_id': '1043590',
            'user_name': 'jode@doe.com',
            'task_id': '92355524',
            'task_note': '',
            'last_modify': '2022-05-26 15:26:54',
            'date': '2022-05-26',
            'start_time': '13:31:11',
            'end_time': '15:26:54',
            'locked': '0',
            'name': 'SoftwareDevelopment',
            'addons_external_id': '',
            'billable': 1,
            'invoiceId': '0',
            'color': '#E9A6B7',
            'description': 'ID-007 Drinking tea'
          },
          {
            'id': 144996221,
            'duration': '2286',
            'user_id': '1043590',
            'user_name': 'jode@doe.com',
            'task_id': '92355524',
            'task_note': '',
            'last_modify': '2022-05-26 18:08:11',
            'date': '2022-05-26',
            'start_time': '15:26:54',
            'end_time': '16:05:00',
            'locked': '0',
            'name': 'SoftwareDevelopment',
            'addons_external_id': '',
            'billable': 1,
            'invoiceId': '0',
            'color': '#E9A6B7',
            'description': 'ID-007 Debugging data binding'
          },
          {
            'id': 145008034,
            'duration': '3476',
            'user_id': '1043590',
            'user_name': 'jode@doe.com',
            'task_id': '92355524',
            'task_note': '',
            'last_modify': '2022-05-30 08:26:46',
            'date': '2022-05-26',
            'start_time': '17:10:17',
            'end_time': '18:08:13',
            'locked': '0',
            'name': 'SoftwareDevelopment',
            'addons_external_id': '',
            'billable': 1,
            'invoiceId': '0',
            'color': '#E9A6B7',
            'description': 'ID-007 Solving problem with cleaning up  sequence during undo operation'
          }
        }";
    }
}
