﻿using RouletteApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RouletteApi.Data
{
    public class MockBetRepository : IBetRepository
    {
        private readonly RouletteApiContext _context;
        public MockBetRepository(RouletteApiContext context)
        {
            _context = context;
        }

        public void CreateBet(Bet bt)
        {
            if (bt == null)
            {
                throw new ArgumentNullException(nameof(bt));
            }

            _context.Bet.Add(bt);
        }

        public IEnumerable<Bet> GetBets(int id)
        {
            var bets = _context.Bet.Where(x => x.RouletteId.Equals(id));

            return bets;
        }

        public object GetRouletteById(int id)
        {
            return _context.Roulette.FirstOrDefault(p => p.Id == id);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdateStatusRoulette(int id)
        {
            var roulette = _context.Roulette.FirstOrDefault(p => p.Id == id);
            if (roulette != null)
            {
                roulette.Status = false;
                _context.Roulette.Update(roulette);
                _context.SaveChanges();
            }
        }
    }
}
