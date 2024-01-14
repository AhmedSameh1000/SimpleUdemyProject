using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyProject.Contracts.DTOs.LectureDTOs;

namespace UdemyProject.Contracts.DTOs.SectionDTOs
{
    public class SectionForReturnDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string WhatStudentLearnFromthisSection { get; set; }
        public int CourseId { get; set; }
        public List<LectureForReturnDTO> Lectures { get; set; }
    }
}