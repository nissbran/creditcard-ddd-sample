﻿namespace Bank.Cards.Domain.Card.Events
{
    using System;

    [EventType("CardAuthorized")]
    public class CardAuthorizedEvent
    {
        public decimal ReservedAmount { get; set; }

        public DateTimeOffset ReservationExpires { get; set; }
    }
}