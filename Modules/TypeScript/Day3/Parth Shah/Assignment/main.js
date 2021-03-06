"use strict";
exports.__esModule = true;
var company_1 = require("./company");
var storingResult_1 = require("./storingResult");
var ApplicantsData_1 = require("./ApplicantsData");
var interview_1 = require("./interview");
var n = new company_1.checkingVacancy();
var m = new interview_1.checkingInterview();
var o = new ApplicantsData_1.checkingApplicants();
var p = new storingResult_1.checkingHiring();
//make switch case here:
var op = 10; //you can change your case from here
switch (op) {
    //vacancy detail 
    case 1:
        console.log('This will carry out the name of that department which has higher vacancies than 15 and selected for interview:');
        n.vacancyAvailaible();
        break;
    case 2:
        console.log('Normally list out the department and their vancancies :');
        n.checkDept();
        break;
    case 3:
        console.log('check vacancies by specific id :');
        n.checkById(2);
        break;
    //interview details
    case 4:
        console.log('List out the interview detail:');
        m.interviewDetail();
        break;
    case 5:
        console.log('checking sppecific Interview handler name for specific department interview');
        m.checkByName('node');
        break;
    case 6:
        console.log('checking interview date for any particular department interview :');
        m.checkDate('QA');
        break;
    //storing applicants data detail :
    case 7:
        console.log('List the data of Applicants for interview');
        o.appicantsDetail();
        break;
    case 8:
        console.log('Get details of applicants by its ID');
        o.applicantsById(2);
        break;
    case 9:
        console.log('Get details of applicants of any particular field like .net , node etc');
        o.isSelected();
        break;
    //get the interview output
    case 10:
        console.log('Check result who are selected in interview');
        console.log('Those candidate are selected whoose mark is above or equal to 30:');
        p.isHired();
        break;
    default:
        console.log("No such number exists!");
        break; // case 11 
}
