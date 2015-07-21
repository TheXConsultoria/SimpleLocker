using System;
using System.Threading;

namespace SL
{
	public sealed class SimpleLocker
	{
		private readonly ReaderWriterLockSlim _locker = new ReaderWriterLockSlim();
		private int _lockTimeout = 1000;

		public SimpleLocker(int lockTimeout = 1000)
		{
			_lockTimeout = lockTimeout;
		}

		public ReturnType Read<ReturnType>(Func<ReturnType> what)
		{
			if (!_locker.TryEnterReadLock(_lockTimeout)) { throw new TimeoutException(); }
			try
			{
				var result = what();
				_locker.ExitReadLock();
				return result;
			}
			catch
			{
				if (_locker.IsReadLockHeld)
				{
					_locker.ExitReadLock();
				}
				throw;
			}
		}

		public ReturnType UpgradeableRead<ReturnType>(Func<ReturnType> what)
		{
			if (!_locker.TryEnterUpgradeableReadLock(_lockTimeout)) { throw new TimeoutException(); }
			try
			{
				var result = what();
				_locker.ExitUpgradeableReadLock();
				return result;
			}
			catch
			{
				if (_locker.IsUpgradeableReadLockHeld)
				{
					_locker.ExitUpgradeableReadLock();
				}
				throw;
			}
		}

		public ReturnType Write<ReturnType>(Func<ReturnType> what)
		{
			if (!_locker.TryEnterWriteLock(_lockTimeout)) { throw new TimeoutException(); }
			try
			{
				var result = what();
				_locker.ExitWriteLock();
				return result;
			}
			catch
			{
				if (_locker.IsWriteLockHeld)
				{
					_locker.ExitWriteLock();
				}
				throw;
			}
		}

		public void ReadVoid(Action what)
		{
			if (!_locker.TryEnterReadLock(_lockTimeout)) { throw new TimeoutException(); }
			try
			{
				what();
				_locker.ExitReadLock();
			}
			catch
			{
				if (_locker.IsReadLockHeld)
				{
					_locker.ExitReadLock();
				}
				throw;
			}
		}

		public void UpgradeableReadVoid(Action what)
		{
			if (!_locker.TryEnterUpgradeableReadLock(_lockTimeout)) { throw new TimeoutException(); }
			try
			{
				what();
				_locker.ExitUpgradeableReadLock();
			}
			catch
			{
				if (_locker.IsUpgradeableReadLockHeld)
				{
					_locker.ExitUpgradeableReadLock();
				}
				throw;
			}
		}

		public void WriteVoid(Action what)
		{
			if (!_locker.TryEnterWriteLock(_lockTimeout)) { throw new TimeoutException(); }
			try
			{
				what();
				_locker.ExitWriteLock();
			}
			catch
			{
				if (_locker.IsWriteLockHeld)
				{
					_locker.ExitWriteLock();
				}
				throw;
			}
		}
	}

}