﻿using MindSpace.Domain.Entities.Appointments;
using MindSpace.Domain.Entities.Constants;
using System;
using System.Linq.Expressions;

namespace MindSpace.Application.Specifications.AppointmentSpecifications
{
    public class AppointmentSpecification : BaseSpecification<Appointment>
    {
        public enum StringParameterType
        {
            StudentEmail,
            SessionId,
            PsychologistEmail
        }

        public enum IntParameterType
        {
            MeetingRoomId,
            AppointmentId,
        }
        /// <summary>
        /// Specification for filtering appointments by student email, session ID, or appointment reference.
        /// </summary>
        /// <param name="value">The string value to filter by</param>
        /// <param name="parameterType">Specifies the type of the string parameter</param>
        public AppointmentSpecification(string value, StringParameterType parameterType)
            : base(GetFilterExpression(value, parameterType))
        {
            AddInclude(a => a.PsychologistSchedule);
            AddInclude(a => a.Psychologist);
            AddInclude(a => a.Student);
        }



        /// <summary>
        /// Specification for filtering appointments by student, psychologist and schedule IDs.
        /// Only returns pending appointments and includes the related PsychologistSchedule.
        /// </summary>
        /// <param name="studentId">The student ID to filter by</param>
        /// <param name="psychologistId">The psychologist ID to filter by</param>
        /// <param name="scheduleId">The schedule ID to filter by</param>
        public AppointmentSpecification(int studentId, int psychologistId, int scheduleId, int ExpireTimeInMinutes)
            : base(
                a => a.StudentId == studentId &&
                a.PsychologistId == psychologistId &&
                a.PsychologistScheduleId == scheduleId &&
                a.Status == AppointmentStatus.Pending &&
                a.CreateAt >= DateTime.Now.AddMinutes(-ExpireTimeInMinutes)
            )
        {
        }

        /// <summary>
        /// Specification for filtering appointments by appointment ID, meeting room ID, or student ID.
        /// </summary>
        /// <param name="appointmentId">The appointment ID to filter by</param>
        /// <param name="parameterType">Specifies the type of the integer parameter</param>
        public AppointmentSpecification(int appointmentId, IntParameterType parameterType)
            : base(GetFilterExpression(appointmentId, parameterType))
        {
        }

        public AppointmentSpecification(int studentId, int psychologistId, int scheduleId)
            : base(
                a => a.StudentId == studentId &&
                a.PsychologistId == psychologistId &&
                a.PsychologistScheduleId == scheduleId &&
                a.Status == AppointmentStatus.Pending
            )
        {
            AddInclude(a => a.PsychologistSchedule);
        }

        private static Expression<Func<Appointment, bool>>? GetFilterExpression(int appointmentId, IntParameterType parameterType)
        {
            return parameterType switch
            {
                IntParameterType.AppointmentId => a => a.Id == appointmentId,
                IntParameterType.MeetingRoomId => a => a.MeetingRoomId == appointmentId,
                _ => null
            };
        }
        private static Expression<Func<Appointment, bool>> GetFilterExpression(string value, StringParameterType parameterType)
        {
            return parameterType switch
            {
                StringParameterType.StudentEmail => a => a.Student.Email == value,
                StringParameterType.SessionId => a => a.SessionId == value,
                StringParameterType.PsychologistEmail => a => a.Psychologist.Email == value,
                _ => throw new ArgumentException($"Unsupported parameter type: {parameterType}", nameof(parameterType))
            };
        }
    }
}
